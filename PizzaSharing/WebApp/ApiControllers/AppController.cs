using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App;
using DAL.App.DTO;
using Domain;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public AppController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<UserDashboardDTO>> Dashboard()
        {
            return new UserDashboardDTO
            {
                Loans = await _uow.Loans.AllUserGivenLoans(User.GetUserId()),
                Debts = await _uow.Loans.AllUserTakenLoans(User.GetUserId()),
                OpenReceipts = await _uow.Receipts.AllUserReceipts(User.GetUserId(), false),
                ClosedReceipts = await _uow.Receipts.AllUserReceipts(User.GetUserId(), true)
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptAllDTO>> Receipt(int id)
        {
            //TODO: security check
            var receipt = await _uow.Receipts.FindAsync(id);
            if (receipt == null) return NotFound();
            var rows = await _uow.ReceiptRows.AllReceiptsRows(receipt.Id, receipt.CreatedTime);
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

        [HttpPost]
        //Only used for initial row adding
        public async Task<ActionResult<ReceiptRowAllDTO>> AddReceiptRow(ReceiptRowAllDTO receiptRowDTO)
        {
            if (receiptRowDTO.ReceiptRowId != null) return BadRequest("This action is not used to edit existing rows.");
            if (receiptRowDTO.ReceiptId == null) return BadRequest("Receipt Id not specified");
            var receipt = await _uow.Receipts.FindAsync(receiptRowDTO.ReceiptId);
            //TODO: Security check

            //Make sure data is valid
            if (
                receipt == null ||
                receiptRowDTO.Product?.ProductId == null ||
                await _uow.Products.FindAsync(receiptRowDTO.Product.ProductId) == null ||
                receiptRowDTO.Amount == null ||
                receiptRowDTO.Amount < 0 ||
                receiptRowDTO.Discount.HasValue &&
                (receiptRowDTO.Discount.Value < 0.0m || receiptRowDTO.Discount.Value > 1.0m))
            {
                return BadRequest("Invalid receiptId, productId, amount or discount value");
            }

            //1. Add ReceiptRow
            var rowMin = new ReceiptRowMinDTO()
            {
                Amount = receiptRowDTO.Amount.Value,
                Discount = receiptRowDTO.Discount,
                ProductId = receiptRowDTO.Product.ProductId.Value,
                ReceiptId = receiptRowDTO.ReceiptId.Value
            };
            var receiptRow = await _uow.ReceiptRows.AddAsync(rowMin);

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
            
            //Loaded - receipt, receipt row, product
            await _uow.SaveChangesAsync();
            var row = await _uow.ReceiptRows.FindRowAndRelatedDataAsync(receiptRow.Id);

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

        [HttpPost]
        public async Task<ActionResult<ReceiptSendDTO>> NewReceipt()
        {
            var receiptDTO = new ReceiptDTO()
            {
                ReceiptManagerId = User.GetUserId(),
                CreatedTime = DateTime.Now,
                IsFinalized = false
            };
            var receipt = await _uow.Receipts.AddAsync(receiptDTO);
            await _uow.SaveChangesAsync();
            
            return new ReceiptSendDTO()
            {
                ReceiptId = receipt.Id,
                CreatedTime = receipt.CreatedTime,
                IsFinalized = receipt.IsFinalized,
                SumCost = decimal.Zero
            };
        }

        [HttpGet("{receiptId}")]
        public async Task<ActionResult<List<OrganizationDTO>>> Organizations(int receiptId)
        {
            //TODO: get date time from receipt 
            var receipt = await _uow.Receipts.FindAsync(receiptId);
            if (receipt == null) return BadRequest("Receipt doesn't exist!");
            return await _uow.Organizations.AllDtoAsync(receipt.CreatedTime);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAmount(ReceiptRowAmountChangeDTO dto)
        {
            //TODO: security check
            var row = await _uow.ReceiptRows.FindAsync(dto.ReceiptRowId);
            if (row == null) return BadRequest("ReceiptRow not found");
            row.Amount = dto.NewAmount;
            await _uow.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost("{rowId}")]
        public async Task<ActionResult> DeleteRow(int rowId)
        {
            //TODO: security check
            var rowExists = await _uow.ReceiptRows.Exists(rowId);
            if (!rowExists) return BadRequest("ReceiptRow not found");
            _uow.ReceiptRows.Remove(id: rowId);
            await _uow.SaveChangesAsync();
            return Ok();
        }
    }
}