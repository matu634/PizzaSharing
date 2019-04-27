using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.Category;
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

        [HttpGet("dashboard/create")]
        public async Task<IActionResult> Create()
        {
            return View(new CreateOrganizationViewModel());
        }


        [HttpPost("dashboard/create")]
        public async Task<IActionResult> Create(CreateOrganizationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var organization = new BLLOrganizationMinDTO
                {
                    Name = vm.Name
                };

                var result = await _bll.OrganizationsService.AddOrganizationAsync(organization);
                if (result == false) return BadRequest("Something went wrong while adding the organization");
                return RedirectToAction("Index", "Dashboard");
            }

            return BadRequest("Model invalid");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                key: CookieRequestCultureProvider.DefaultCookieName,
                value: CookieRequestCultureProvider.MakeCookieValue(
                    requestCulture: new RequestCulture(culture: culture)),
                options: new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(years: 1)
                }
            );

            return LocalRedirect(localUrl: returnUrl);
        }
    }
}