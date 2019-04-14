using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App;
using DAL.App.DTO;
using Domain;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                OpenReceipts = await _uow.Receipts.  AllUserReceipts(User.GetUserId(), false),
                ClosedReceipts = await _uow.Receipts.AllUserReceipts(User.GetUserId(), true) 
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptAllDTO>> Receipt(int id)
        {
            //TODO: security check
            var receipt = await _uow.Receipts.FindAsync(id);
            if (receipt == null) return NotFound();
            var result = new ReceiptAllDTO()
            {
                ReceiptId = receipt.Id,
                CreatedTime = receipt.CreatedTime,
                IsFinalized = receipt.IsFinalized,
                Rows = await _uow.ReceiptRows.AllReceiptsRows(receipt.Id, receipt.CreatedTime)
            };
            return result;
        }

        [HttpPost]
        //Only used for initial row adding
        public async Task<IActionResult> AddReceiptRow(ReceiptRowAllDTO receiptRowDTO)
        {
            if (receiptRowDTO.ReceiptRowId != null) return BadRequest();
            if (receiptRowDTO.ReceiptId == null) return BadRequest();
            var receipt = await _uow.Receipts.FindAsync(receiptRowDTO.ReceiptId);
            
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
                return BadRequest();
            }
            
            //Add ReceiptRow
            var rowMin = new ReceiptRowMinDTO()
            {
                Amount = receiptRowDTO.Amount.Value,
                Discount = receiptRowDTO.Discount,
                ProductId = receiptRowDTO.Product.ProductId.Value,
                ReceiptId = receiptRowDTO.ReceiptId.Value
            };
            var row = await _uow.ReceiptRows.AddAsync(rowMin);
            
            //TODO: saving here to get row id, maybe remove to keep UnitOfWork happy
//            await _uow.SaveChangesAsync();
            
            //Add row changes (optional)
            if (receiptRowDTO.Changes != null && receiptRowDTO.Changes.Count > 0)
            {
                foreach (var changeDTO in receiptRowDTO.Changes)
                {
                    if (changeDTO.ChangeId == null ||
                        await _uow.Changes.FindAsync(changeDTO.ChangeId) == null)
                    {
                        return BadRequest();
                    }

                    changeDTO.ReceiptRowId = row.Id;
                    await _uow.ReceiptRowChanges.AddAsync(changeDTO);
                }
            }
            //Add participants (optional)
            /*
            if (receiptRowDTO.Participants != null && receiptRowDTO.Participants.Count > 0)
            {   
                foreach (var participant in receiptRowDTO.Participants)
                {
                    _uow.ReceiptParticipants.FindOrAddAsync(receipt.Id, participant.AppUserId);
                }
            }
            */
            
            await _uow.SaveChangesAsync();
            return Ok();
        }
    }
}