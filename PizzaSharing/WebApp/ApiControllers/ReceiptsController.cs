using System.Threading.Tasks;
using Contracts.BLL.App;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;
using PublicApi.DTO.Mappers;

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
            var receiptDto = await _bll.ReceiptsService.GetReceiptAndRelatedData(receiptId, User.GetUserId());
            if (receiptDto == null) return Unauthorized();
            var result = ReceiptMapper.FromBLLToAll(receiptDto);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptRowAllDTO>> AddRow(ReceiptRowMinDTO receiptRowDTO)
        {
            if (receiptRowDTO == null) return BadRequest("DTO missing");
            var bllDto = ReceiptRowMapper.FromAPI2(receiptRowDTO);
            if (bllDto == null) return BadRequest("Data missing from dto");
            
            var receiptRowDto = await _bll.ReceiptsService.AddRow(bllDto, User.GetUserId());
            if (receiptRowDto == null) return BadRequest("Something went wrong while adding");
            return ReceiptRowMapper.FromBLL(receiptRowDto);
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
            if (dto == null) return BadRequest("DTO missing");
            var receiptRow = await _bll.ReceiptsService.UpdateRowAmount(ReceiptRowMapper.FromAPI(dto), User.GetUserId());
            if (receiptRow == null) return BadRequest();
            return ReceiptRowMapper.FromBLL(receiptRow);
        }
    }
}