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
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public AppController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<UserDashboardDTO>> Dashboard()
        {
            var userDashboard = await _bll.AppService.GetUserDashboard(User.GetUserId());
            if (userDashboard == null) return BadRequest("Unknown error occurred");
            
            return UserDashboardMapper.FromBLL(userDashboard);
        }

        [HttpGet("{receiptId}")]
        public async Task<ActionResult<List<OrganizationDTO>>> Organizations(int receiptId)
        {
            var organizations = await _bll.AppService.GetOrganizationsCategoriesAndProductsAsync(receiptId);
            if (organizations == null) return BadRequest();
            return organizations
                .Select(OrganizationMapper.FromBLL)
                .ToList();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<List<ChangeDTO>>> ProductChanges(int productId)
        {
            var changes = await _bll.ProductService.GetProductChangesAsync(productId);
            if (changes == null) return BadRequest();
            return changes
                .Select(ChangeMapper.FromBLL)
                .ToList();
        }
    }
}