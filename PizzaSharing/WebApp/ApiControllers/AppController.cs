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
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            return await _bll.AppService.GetUserDashboard(User.GetUserId());
        }

        [HttpGet("{receiptId}")]
        public async Task<ActionResult<List<OrganizationDTO>>> Organizations(int receiptId)
        {
            var result = await _bll.AppService.GetOrganizationsCategoriesAndProductsAsync(receiptId);
            if (result == null) return BadRequest();
            return result;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<List<ChangeDTO>>> ProductChanges(int productId)
        {
            var changes = await _bll.ProductService.GetProductChangesAsync(productId);
            if (changes == null) return BadRequest();
            return changes
                .Select(dto => new ChangeDTO()
                {
                    Price = dto.CurrentPrice,
                    Name = dto.Name,
                    ChangeId = dto.Id
                })
                .ToList();
        }
    }
}