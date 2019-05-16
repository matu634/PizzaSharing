using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.masirg.BLL.Base.Services;
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

            var receiptDTO = ReceiptMapper.FromBLL(userId);
            
            var receiptId = await Uow.Receipts.AddAsync(receiptDTO);
            if (receiptId == null) return -1;
            await Uow.SaveChangesAsync();

            var result = Uow.Receipts.GetEntityIdAfterSaveChanges(receiptId.Value); 
            if (result == null) return -1;
            return result.Value;
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
            Uow.Receipts.Remove(receiptId); //Hard delete
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

        public async Task<BLLReceiptRowDTO> AddRowChange(int rowId, int changeId, int userId)
        {
            var receiptRow = await Uow.ReceiptRows.FindAsync(rowId);
            if (receiptRow?.ReceiptId == null) return null;
            if (receiptRow.ProductId == null) throw new Exception("Product id is null");

            var receipt = await Uow.Receipts.FindReceiptAsync(receiptRow.ReceiptId.Value);
            if (receipt.ReceiptManagerId != userId || receipt.IsFinalized) return null;
            
            var change = await Uow.Changes.FindDTOAsync(changeId);
            if (change == null) return null;
            if (change.Categories == null) throw new Exception("Change categories list is null(not loaded/mapped)");

            var product = await Uow.Products.FindDTOAsync(receiptRow.ProductId.Value);
            if (product?.Categories == null) throw new Exception("Product or its categories list is null (not loaded/mapped)");

            var productCategories = product.Categories.Select(dto => dto.Id).ToList();
            var changeCategories = change.Categories.Select(dto => dto.Id).ToList();
            
            //Check if product and change have common categories
            if (!productCategories.Intersect(changeCategories).Any()) return null;

            await Uow.ReceiptRowChanges.AddAsync(changeId, rowId);
            await Uow.SaveChangesAsync();

            receiptRow = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(rowId);
            return ReceiptRowMapper.FromDAL(receiptRow);
        }

        public async Task<BLLReceiptRowDTO> AddRowParticipantAsync(BLLRowParticipantDTO newParticipant, int currentUserId)
        {
            //Involvement is between 0 and 1
            if (newParticipant.Involvement == null ||
                newParticipant.Involvement > 1 ||
                newParticipant.Involvement <= 0) return null;
            if (newParticipant.AppUserId == null) return null;
            
            //Participant user exists
            if (!await Uow.AppUsers.Exists(newParticipant.AppUserId.Value)) return null;

            var receiptRow = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(newParticipant.ReceiptRowId);
            if (receiptRow?.ReceiptId == null) return null;

            var receipt = await Uow.Receipts.FindReceiptAsync(receiptRow.ReceiptId.Value);
            
            
            if (receipt?.ReceiptParticipants == null) return null;
            if (receipt.ReceiptManagerId != currentUserId || receipt.IsFinalized) return null;

            
            if (receiptRow.Participants != null && receiptRow.Participants.Any())
            {
                //Participant can't already be a participant in this row
                if (receiptRow.Participants.Any(dto => dto.AppUserId == newParticipant.AppUserId)) return null;
                    
                var currentInvolvementSum = receiptRow.Participants.Select(dto => dto.Involvement).Sum();
                //Total involvement can't be greater than 1
                if (currentInvolvementSum == null || currentInvolvementSum + newParticipant.Involvement > 1) return null;
            }

            var receiptParticipant = await Uow.ReceiptParticipants.FindOrAddAsync(receipt.ReceiptId, newParticipant.AppUserId.Value);
            
            var loanId = await Uow.Loans.FindOrAddAsync(receiptParticipant, receipt.ReceiptManagerId);

            await Uow.LoanRows.AddAsync(loanId, receiptRow.ReceiptRowId.Value, newParticipant.Involvement.Value);
            await Uow.SaveChangesAsync();
            
            receiptRow = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(newParticipant.ReceiptRowId);
            return ReceiptRowMapper.FromDAL(receiptRow);
        }

        public async Task<BLLReceiptRowDTO> RemoveRowChangeAsync(int rowId, int changeId, int userId)
        {
            var receiptRow = await Uow.ReceiptRows.FindAsync(rowId);
            if (receiptRow?.ReceiptId == null) return null;

            var receipt = await Uow.Receipts.FindReceiptAsync(receiptRow.ReceiptId.Value);
            if (receipt.ReceiptManagerId != userId || receipt.IsFinalized) return null;
            
            var change = await Uow.Changes.FindDTOAsync(changeId);
            if (change == null) return null;

            

            var removedSuccessfully = await Uow.ReceiptRowChanges.RemoveWhereAsync(changeId, rowId);
            if (!removedSuccessfully) return null;
            await Uow.SaveChangesAsync();

            receiptRow = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(rowId);
            return ReceiptRowMapper.FromDAL(receiptRow);
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

        public async Task<List<BLLAppUserDTO>> GetAvailableRowParticipants(int rowId, int userId)
        {
            //TODO: leaner find method, just needs app user ids
            var row = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(rowId);
            if (row == null) return null;

            var rowParticipants = row.Participants
                .Select(AppUserMapper.FromDALParticipantDTO)
                .ToList();
            rowParticipants.Add(new BLLAppUserDTO(){Id = userId});
            
            var allUsers = (await Uow.AppUsers.AllAsync())
                .Select(AppUserMapper.FromDAL)
                .ToList();

            return allUsers.Except(rowParticipants).ToList();
        }
    }
}