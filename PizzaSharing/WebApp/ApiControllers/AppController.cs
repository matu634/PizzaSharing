using System;
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
    /// Endpoints that handle initial essential data
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public AppController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all User's open receipts, closed receipts, debts and loans
        /// </summary>
        /// <returns></returns>
        /// <response code="200">User related data successfully retrieved.</response>
        /// <response code="400">Something unexpected happened.</response>
        [HttpGet]
        public async Task<ActionResult<UserDashboardDTO>> Dashboard()
        {
            var userDashboard = await _bll.AppService.GetUserDashboard(User.GetUserId());
            if (userDashboard == null) return BadRequest("Unknown error occurred");
            
            return UserDashboardMapper.FromBLL(userDashboard);
        }
        /// <summary>
        /// Gets all organizations with all their Categories which include all their Products
        /// </summary>
        /// <param name="receiptId">Receipt currently being edited</param>
        /// <returns>OrganizationDTO</returns>
        /// <response code="200">Organizations were successfully retrieved.</response>
        /// <response code="400">Something went wrong while retrieving organizations(Most likely receiptId is invalid).</response>
        [HttpGet("{receiptId}")]
        public async Task<ActionResult<List<OrganizationDTO>>> Organizations(int receiptId)
        {
            var organizations = await _bll.AppService.GetOrganizationsCategoriesAndProductsAsync(receiptId);
            if (organizations == null) return BadRequest();
            return organizations
                .Select(OrganizationMapper.FromBLL)
                .ToList();
        }
    }
}