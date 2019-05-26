using System.Threading.Tasks;
using Contracts.BLL.App;
using Enums;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoansController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public LoansController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Changes a loan's status
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>NewStatusCode if successful, if data is invalid returns -1</returns>
        [HttpPost]
        public async Task<ActionResult<int>> ChangeLoanStatus(LoanIdAndStatus dto)
        {
            if (dto == null) return BadRequest("LoanIdAndStatus is missing");
            if (dto.Status == null || dto.LoanId == null) return BadRequest("LoanIdAndStatus is missing"); 
            var newStatus = await _bll.LoanService.ChangeLoanStatusAsync(dto.LoanId.Value, dto.Status.Value , User.GetUserId());
            return newStatus;
        }
    }
}