using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain.Identity;
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

        [HttpGet("{receiptId}")]
        public async Task<ActionResult<List<OrganizationDTO>>> Organizations(int receiptId)
        {
            //TODO: get date time from receipt 
            var receipt = await _uow.Receipts.FindAsync(receiptId);
            if (receipt == null) return BadRequest("Receipt doesn't exist!");
            return await _uow.Organizations.AllDtoAsync(receipt.CreatedTime);
        }
    }
}