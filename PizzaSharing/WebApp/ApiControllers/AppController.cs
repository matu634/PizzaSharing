using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
        private readonly IAppBLL _bll;

        public AppController(IAppUnitOfWork uow, IAppBLL bll)
        {
            _uow = uow;
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<UserDashboardDTO>> Dashboard()
        {
            return await _bll.AppService.GetUserDashboard(User.GetUserId());
        }

        /*
        [HttpPost]
        public async Task<ActionResult<ReceiptAllDTO>> NewReceipt()
        {
            var receiptDTO = new ReceiptDTO()
            {
                ReceiptManagerId = User.GetUserId(),
                CreatedTime = DateTime.Now,
                IsFinalized = false
            };
            var receipt = await _uow.Receipts.AddAsync(receiptDTO);
            await _uow.SaveChangesAsync();
            
            return new ReceiptAllDTO()
            {
                ReceiptId = receipt.Id,
                CreatedTime = receipt.CreatedTime,
                IsFinalized = receipt.IsFinalized,
                SumCost = decimal.Zero,
                Rows = new List<ReceiptRowAllDTO>()
            };
        }
        */

        [HttpGet("{receiptId}")]
        public async Task<ActionResult<List<OrganizationDTO>>> Organizations(int receiptId)
        {
            //TODO: get date time from receipt 
            var receipt = await _uow.Receipts.FindAsync(receiptId);
            if (receipt == null) return BadRequest("Receipt doesn't exist!");
            return await _uow.Organizations.AllDtoAsync(receipt.CreatedTime);
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptRowAllDTO>> UpdateRowAmount(ReceiptRowAmountChangeDTO dto)
        {
            //TODO: security check
            var row = await _uow.ReceiptRows.FindRowAndRelatedDataAsync(dto.ReceiptRowId);
            if (row == null) return BadRequest("ReceiptRow not found");
            row.Amount = dto.NewAmount;
            await _uow.SaveChangesAsync();
            
            //TODO: mapper
            var result = new ReceiptRowAllDTO()
            {
                Amount = row.Amount,
                Product = new ProductDTO()
                {
                    ProductId = row.Product.Id,
                    ProductName = row.Product.ProductName,
                    ProductPrice = row.Product.GetPriceAtTime(row.Receipt.CreatedTime)                    
                },
                CurrentCost = row.RowSumCost(),
                Changes = row.ReceiptRowChanges.Select(rowChange =>
                {
                    return new ChangeDTO()
                    {
                        Name = rowChange.Change.ChangeName,
                        Price = rowChange.Change.GetPriceAtTime(row.Receipt.CreatedTime),
                        ChangeId = rowChange.ChangeId,
                        CategoryId = rowChange.Change.CategoryId,
                        OrganizationId = rowChange.Change.OrganizationId,
                        ReceiptRowId = rowChange.ReceiptRowId
                    };
                }).ToList(),
                Participants = row.RowParticpantLoanRows.Select(loanRow =>
                {
                    return new RowParticipantDTO()
                    {
                        Name = loanRow.Loan.LoanTaker.UserNickname,
                        Involvement = loanRow.Involvement,
                        ReceiptRowId = row.Id,
                        LoanId = loanRow.LoanId,
                        AppUserId = loanRow.Loan.LoanTakerId,
                        LoanRowId = loanRow.Id
                    };
                }).ToList(),
                ReceiptId = row.ReceiptId,
                ReceiptRowId = row.Id,
                Discount = row.RowDiscount
            };
            return result;
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

        //Submit receipt(mark finalized)

        //Add participant
        //Add change
        
        //Get all products changes
        //Get all participants

        //Mark debt paid
        //confirm loan paid
        //decline loan paid
        //mark loan paid

    }
}