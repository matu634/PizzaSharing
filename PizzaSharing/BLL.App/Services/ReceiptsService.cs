using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using PublicApi.DTO;

namespace BLL.App.Services
{
    public class ReceiptsService : BaseService<IAppUnitOfWork>, IReceiptsService
    {
        public ReceiptsService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<BLLReceiptDTO> GetReceiptAndRelatedData(int receiptId, int currentUserId)
        {
            var receipt = await Uow.Receipts.FindReceiptAsync(receiptId);

            if (receipt == null) return null;
            //Check if current user is manager or participant
            if (receipt.ReceiptManagerId != currentUserId && !receipt.ReceiptParticipants
                    .Select(dto => dto.ParticipantAppUserId).ToList().Contains(currentUserId)) return null;

            var time = receipt.IsFinalized == false ? DateTime.Now : receipt.CreatedTime;
            var rows = await Uow.ReceiptRows.AllReceiptsRows(receipt.ReceiptId, time);
            return ReceiptMapper.FromDAL2(receipt, rows);
        }

        public async Task<int> NewReceipt(int userId)
        {
            //TODO: mapper
            var receiptDTO = new ReceiptDTO()
            {
                ReceiptManagerId = userId,
                CreatedTime = DateTime.Now,
                IsFinalized = false
            };
            var receipt = await Uow.Receipts.AddAsync(receiptDTO);
            await Uow.SaveChangesAsync();

            return receipt.Id;
        }
        /// <summary>
        ///  Permanently removes receipt, if it is not finalized and currentUser is receiptManager
        /// </summary>
        /// <param name="receiptId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveReceipt(int receiptId, int currentUserId)
        {
            var receipt = await Uow.Receipts.FindReceiptAsync(receiptId);
            if (receipt == null || receipt.IsFinalized || receipt.ReceiptManagerId != currentUserId) return false;
            Uow.Receipts.Remove(receipt); //Hard delete
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<BLLReceiptRowDTO> AddRow(BLLReceiptRowDTO receiptRowDTO, int currentUserId)
        {
            if (receiptRowDTO == null) return null;
            
            var receiptRow = ReceiptRowMapper.FromBLL(receiptRowDTO);
            
            if (receiptRow == null) return null;
            if (receiptRow.Amount < 0) return null;
            if (receiptRow.Discount.HasValue && (receiptRow.Discount.Value < 0.0m || receiptRow.Discount.Value > 1.0m)) return null;
            if (!receiptRow.ReceiptId.HasValue) return null;
            if (!receiptRow.ProductId.HasValue) return null;

            var receipt = await Uow.Receipts.FindReceiptAsync(receiptRow.ReceiptId.Value);
            var product = await Uow.Products.FindDTOAsync(receiptRow.ProductId.Value);

            if (receipt == null || receipt.IsFinalized || receipt.ReceiptManagerId != currentUserId) return null;
            if (product == null) return null;

            
            
            var addedRowId = await Uow.ReceiptRows.AddAsync(receiptRow);
            if (addedRowId == null) return null;
            
            await Uow.SaveChangesAsync();
            var newId = Uow.ReceiptRows.GetEntityIdAfterSaveChanges(addedRowId.Value);
            if (newId == null) return null;
            var row = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(newId.Value);
            return ReceiptRowMapper.FromDAL(row);
        }

        public async Task<BLLReceiptRowDTO> UpdateRowAmount(BLLReceiptRowDTO dto, int userId)
        {
            if (dto?.ReceiptRowId == null || dto.Amount == null) return null;
            var rowId = await Uow.ReceiptRows.UpdateRowAmount(dto.ReceiptRowId.Value, dto.Amount.Value, userId);
            if (rowId == null) return null;
            await Uow.SaveChangesAsync();

            var row = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(rowId.Value);
            return ReceiptRowMapper.FromDAL(row);
        }

        public async Task<bool> RemoveRow(int rowId, int userId)
        {
            var deleted = await Uow.ReceiptRows.RemoveRowAsync(rowId, userId);
            if (deleted == false) return false;
            await Uow.SaveChangesAsync();
            return true;
        }

        public ReceiptRowAllDTO AddRowChange()
        {
            throw new System.NotImplementedException();
        }

        public ReceiptRowAllDTO AddRowParticipant()
        {
            throw new System.NotImplementedException();
            /*
              //2. Add row changes (optional)
            if (receiptRowDTO.Changes != null && receiptRowDTO.Changes.Count > 0)
            {
                var validChangeCategoryIds =
                    await _uow.ProductsInCategories.CategoryIdsAsync(productId: receiptRow.ProductId);

                foreach (var changeDTO in receiptRowDTO.Changes)
                {
                    if (changeDTO.ChangeId == null) return BadRequest("Change Id not be found");
                    var change = await _uow.Changes.FindAsync(changeDTO.ChangeId);
                    if (!validChangeCategoryIds.Contains(change.CategoryId))
                        return BadRequest($"ChangeId: {change.Id} not allowed for this product.");

                    changeDTO.ReceiptRowId = receiptRow.Id;
                    await _uow.ReceiptRowChanges.AddAsync(changeDTO);
                }
            }

            //3. Add participants (optional)
            if (receiptRowDTO.Participants != null && receiptRowDTO.Participants.Count > 0)
            {
                //Make sure there is only one entry per appUser
                var uniqueAppUsersCount = receiptRowDTO.Participants.Select(dto => dto.AppUserId).Distinct().Count();
                if (uniqueAppUsersCount != receiptRowDTO.Participants.Count)
                    return BadRequest("Row participant: Only one entry per user is allowed for each row");
                var involvementSum = decimal.Zero;

                foreach (var participantDTO in receiptRowDTO.Participants)
                {
                    if (participantDTO.Involvement == null ||
                        participantDTO.Involvement > 1.0m ||
                        participantDTO.AppUserId == null ||
                        participantDTO.AppUserId == receipt.ReceiptManagerId)
                    {
                        return BadRequest("Invalid involvement or AppUserId");
                    }

                    involvementSum += participantDTO.Involvement.Value;

                    var participant = await _uow.ReceiptParticipants.FindOrAddAsync(receiptId: receipt.Id,
                        loanTakerId: participantDTO.AppUserId.Value);
                    var loan = await _uow.Loans.FindOrAddAsync(participant);

                    participantDTO.LoanId = loan.Id;
                    participantDTO.ReceiptRowId = receiptRow.Id;

                    await _uow.LoanRows.AddAsync(participantDTO);
                }

                if (involvementSum > decimal.One) return BadRequest("Involvement sum over 1.00");
            }
             */
        }

        public ReceiptRowAllDTO RemoveRowChange()
        {
            throw new System.NotImplementedException();
        }

        public ReceiptRowAllDTO RemoveRowParticipant()
        {
            throw new System.NotImplementedException();
        }

        public ReceiptRowAllDTO EditRowParticipantInvolvement()
        {
            throw new System.NotImplementedException();
        }

        public bool SetReceiptFinalized()
        {
            throw new System.NotImplementedException();
        }

        public ReceiptRowAllDTO AddRowDiscount()
        {
            throw new NotImplementedException();
        }

        public ReceiptRowAllDTO RemoveRowDiscount()
        {
            throw new NotImplementedException();
        }
    }
}