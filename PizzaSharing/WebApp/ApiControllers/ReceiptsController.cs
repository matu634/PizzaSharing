using System.Threading.Tasks;
using Contracts.BLL.App;
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
    public class ReceiptsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ReceiptsController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet("{receiptId}")]
        public async Task<ActionResult<ReceiptAllDTO>> Get(int receiptId)
        {
            var result = await _bll.ReceiptsService.GetReceiptAndRelatedData(receiptId, User.GetUserId());
            if (result == null) return Unauthorized();
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptRowAllDTO>> AddRow(ReceiptRowMinDTO receiptRowDTO)
        {
            var result = await _bll.ReceiptsService.AddRow(receiptRowDTO, User.GetUserId());
            if (result == null) BadRequest();
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<int>> NewReceipt()
        {
            return await _bll.ReceiptsService.NewReceipt(User.GetUserId());
        }

        [HttpPost("{receiptId}")]
        public async Task<ActionResult> RemoveReceipt(int receiptId)
        {
            var result = await _bll.ReceiptsService.RemoveReceipt(receiptId, User.GetUserId());
            if (result == false) return BadRequest();
            return Ok();
        }
        
        [HttpPost("{rowId}")]
        public async Task<ActionResult> RemoveRow(int rowId)
        {
            var result = await _bll.ReceiptsService.RemoveRow(rowId: rowId, userId: User.GetUserId());
            if (result == false) return BadRequest();
            return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult<ReceiptRowAllDTO>> UpdateRowAmount(ReceiptRowAmountChangeDTO dto)
        {
            var result = await _bll.ReceiptsService.UpdateRowAmount(dto, User.GetUserId());
            if (result == null) return BadRequest();
            return result;
        }
    }
}