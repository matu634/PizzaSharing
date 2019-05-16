using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Endpoints used to manipulate receipts
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReceiptsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ReceiptsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get receipt and all its related data
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns>ReceiptAllDTO</returns>
        /// <response code="200">Receipt and its related data was retrieved successfully.</response>
        /// <response code="400">Something went wrong while getting data(Most likely receiptId invalid or user is not allowed to access the receipt)</response>
        [HttpGet("{receiptId}")]
        public async Task<ActionResult<ReceiptAllDTO>> Get(int receiptId)
        {
            var receiptDto = await _bll.ReceiptsService.GetReceiptAndRelatedData(receiptId, User.GetUserId());
            if (receiptDto == null) return Unauthorized();
            var result = ReceiptMapper.FromBLLToAll(receiptDto);
            return result;
        }
        
        /// <summary>
        /// Adds a row to the receipt and returns the row
        /// </summary>
        /// <param name="receiptRowDTO"></param>
        /// <returns>ReceiptRowAllDTO</returns>
        /// <response code="200">ReceiptRow was successfully added and retrieved.</response>
        /// <response code="400">Something went wrong while adding the row(ReceiptRowMinDTO might be invalid)</response>
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

        /// <summary>
        /// Adds a new open receipt to the user
        /// </summary>
        /// <returns>Newly added ReceiptId</returns>
        /// <response code="200">Receipt was successfully added.</response>
        [HttpPost]
        public async Task<ActionResult<int>> NewReceipt()
        {
            return await _bll.ReceiptsService.NewReceipt(User.GetUserId());
        }

        /// <summary>
        /// Deletes receipt from the system
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns>Only status code</returns>
        /// <response code="200">Receipt was successfully removed.</response>
        /// <response code="400">Receipt not removed (receiptId might be invalid or user might not be the receipt's manager)</response>
        [HttpPost("{receiptId}")]
        public async Task<ActionResult> RemoveReceipt(int receiptId)
        {
            var result = await _bll.ReceiptsService.RemoveReceipt(receiptId, User.GetUserId());
            if (result == false) return BadRequest();
            return Ok();
        }
        /// <summary>
        /// Deletes receipt row
        /// </summary>
        /// <param name="rowId"></param>
        /// <returns>Only status code</returns>
        /// <response code="200">Receipt's rows was successfully removed.</response>
        /// <response code="400">Receipt's row was not removed (rmowId might be invalid or user might not be the receipt's manager)</response>
        [HttpPost("{rowId}")]
        public async Task<ActionResult> RemoveRow(int rowId)
        {
            var result = await _bll.ReceiptsService.RemoveRow(rowId: rowId, userId: User.GetUserId());
            if (result == false) return BadRequest();
            return Ok();
        }
        /// <summary>
        /// Updates the rows amount and returns the updated row
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Updated receipt row</returns>
        /// <response code="200">Receipt's rows was successfully updated.</response>
        /// <response code="400">Receipt's row was not updated (ReceiptRowAmountChangeDTO might be invalid or user might not be the receipt's manager)</response>
        [HttpPost]
        public async Task<ActionResult<ReceiptRowAllDTO>> UpdateRowAmount(ReceiptRowAmountChangeDTO dto)
        {
            if (dto == null) return BadRequest("DTO missing");
            var receiptRow = await _bll.ReceiptsService.UpdateRowAmount(ReceiptRowMapper.FromAPI(dto), User.GetUserId());
            if (receiptRow == null) return BadRequest();
            return ReceiptRowMapper.FromBLL(receiptRow);
        }
        
        /// <summary>
        ///  Gets all changes that can be applied to a Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>List of ChangeDTOs></returns>
        /// <response code="200">Product changes successfully retrieved.</response>
        /// <response code="400">Something went wrong while retrieving changes(Most likely productId is invalid).</response>
        [HttpGet("{productId}")]
        public async Task<ActionResult<List<ChangeDTO>>> ProductChanges(int productId)
        {
            var changes = await _bll.ProductService.GetProductChangesAsync(productId);
            if (changes == null) return BadRequest();
            return changes
                .Select(ChangeMapper.FromBLL)
                .ToList();
        }

        /// <summary>
        /// Adds a component to the receipt row and returns the updated receipt row
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">Component was successfully added.</response>
        /// <response code="400">Component was not added (changeId, rowId or user might not be valid)</response>
        [HttpPost]
        public async Task<ActionResult<ReceiptRowAllDTO>> AddComponentToRow(RowAndChangeDTO dto)
        {
            if (dto.RowId == null) return BadRequest("rowId is missing");
            if (dto.ComponentId == null) return BadRequest("componentId is missing");
            
            var receiptRow = await _bll.ReceiptsService.AddRowChange(dto.RowId.Value, dto.ComponentId.Value, User.GetUserId());
            if (receiptRow == null) return BadRequest("Component was not added (changeId, rowId or user might not be valid)");
            return ReceiptRowMapper.FromBLL(receiptRow);
        }
        /// <summary>
        /// Removes component from receipt row and returns updated receipt row
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReceiptRowAllDTO>> RemoveComponentFromRow(RowAndChangeDTO dto)
        {
            if (dto.RowId == null) return BadRequest("rowId is missing");
            if (dto.ComponentId == null) return BadRequest("componentId is missing");
            
            var receiptRow = await _bll.ReceiptsService.RemoveRowChangeAsync(dto.RowId.Value, dto.ComponentId.Value, User.GetUserId());
            if (receiptRow == null) return BadRequest("Component was not removed (changeId, rowId or user might not be valid)");
            return ReceiptRowMapper.FromBLL(receiptRow);
        }
    }
}