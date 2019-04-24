using System.Diagnostics;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.Dashboard;

namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAppBLL _bll;

        public DashboardController(IAppBLL bll)
        {
            _bll = bll;
        }

        public async Task<IActionResult> Index()
        {
            var organizationsDTO = await _bll.OrganizationsService.GetOrganizationsMinDTOAsync();
            if (organizationsDTO == null) return BadRequest();
            var vm = new IndexViewModel
            {
                Organizations = organizationsDTO
            };

            return View(vm);
        }

        public async Task<IActionResult> Organization(int id)
        {
            var dto = await _bll.OrganizationsService.GetOrganizationAllDTOAsync(id);
            if (dto == null) return BadRequest();

            var vm = new OrganizationViewModel()
            {
                OrganizationMin = dto.OrganizationMin,
                Changes = dto.Changes,
                Products = dto.Products,
                Categories = dto.Categories
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}