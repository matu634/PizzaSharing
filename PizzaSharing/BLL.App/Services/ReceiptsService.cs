using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.DTO;
using PublicApi.DTO;

namespace BLL.App.Services
{
    public class ReceiptsService : BaseService<IAppUnitOfWork>, IReceiptsService
    {
        public ReceiptsService(IAppUnitOfWork uow) : base(uow)
        {
        }

        public async Task<ReceiptAllDTO> GetReceiptAndRelatedData(int receiptId, int currentUserId)
        {
            var receipt = await Uow.Receipts.FindAsync(receiptId);

            if (receipt == null) return null;
            //Check if current user is manager or participant
            if (receipt.ReceiptManagerId != currentUserId && !receipt.ReceiptParticipants
                    .Select(participant => participant.AppUserId).ToList().Contains(currentUserId)) return null;

            var rows = await Uow.ReceiptRows.AllReceiptsRows(receipt.Id, receipt.CreatedTime);
            var result = new ReceiptAllDTO()
            {
                ReceiptId = receipt.Id,
                CreatedTime = receipt.CreatedTime,
                IsFinalized = receipt.IsFinalized,
                Rows = rows,
                SumCost = rows.Select(dto => dto.CurrentCost ?? decimal.Zero).Sum()
            };
            return result;
        }

        public ReceiptAllDTO NewReceipt()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ReceiptRowAllDTO> AddRow(ReceiptRowMinDTO receiptRowDTO, int currentUserId)
        {
            if (receiptRowDTO.ReceiptId == null) return null;
            var receipt = await Uow.Receipts.FindAsync(receiptRowDTO.ReceiptId);

            //Make sure data is valid
            if (
                receipt == null ||
                receipt.ReceiptManagerId != currentUserId ||
                receiptRowDTO.ProductId == null ||
                await Uow.Products.FindAsync(receiptRowDTO.ProductId) == null ||
                receiptRowDTO.Amount == null ||
                receiptRowDTO.Amount < 0 ||
                receiptRowDTO.Discount.HasValue &&
                (receiptRowDTO.Discount.Value < 0.0m || receiptRowDTO.Discount.Value > 1.0m))
            {
                return null;
            }
            
            //TODO: mapper
            var dalDto = new DALReceiptRowMinDTO()
            {
                Amount = receiptRowDTO.Amount.Value,
                Discount = receiptRowDTO.Discount,
                ProductId = receiptRowDTO.ProductId.Value,
                ReceiptId = receiptRowDTO.ReceiptId.Value
            };
            
            var addedRow = await Uow.ReceiptRows.AddAsync(dalDto);
            
            await Uow.SaveChangesAsync();
            var row = await Uow.ReceiptRows.FindRowAndRelatedDataAsync(addedRow.Id);
            
            //TODO: mapper
            var result = new ReceiptRowAllDTO()
            {
                Amount = row.Amount,
                Product = new ProductDTO()
                {
                    ProductId = row.Product.Id,
                    ProductName = row.Product.ProductName,
                    ProductPrice = row.Product.GetPriceAtTime(receipt.CreatedTime)                    
                },
                CurrentCost = row.RowSumCost(),
                Changes = new List<ChangeDTO>(),
                Participants = new List<RowParticipantDTO>(),
                ReceiptId = row.ReceiptId,
                ReceiptRowId = row.Id,
                Discount = row.RowDiscount
            };
            
            return result;
        }

        public ReceiptRowAllDTO UpdateRowAmount()
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteRow()
        {
            throw new System.NotImplementedException();
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
    }
}