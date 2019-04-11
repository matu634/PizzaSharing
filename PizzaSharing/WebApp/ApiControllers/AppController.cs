using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        
        //[HttpGet]
        //public async Task<ActionResult<UserDashboardDTO>> Loan()
    }
}