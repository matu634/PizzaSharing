using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.Change;

namespace WebApp.Controllers
{
    public class ChangeController : Controller
    {
        private readonly IAppBLL _bll;

        public ChangeController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet("change/create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            var organization = await _bll.OrganizationsService.GetOrganizationWithCategoriesAsync(id);
            if (organization == null) return BadRequest();

            var vm = new CreateChangeViewModel
            {
                OrganizationName = organization.Name,
                OrganizationId = organization.Id,
                Categories = organization.Categories
                    .Select(dto => new SelectListItem(dto.CategoryName, dto.CategoryId.ToString()))
                    .ToList()
            };
            return View(vm);
        }
        
        [HttpPost("change/create/{id}")]
        public async Task<IActionResult> Create(CreateChangeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var changeDto = new BLLChangeDTO()
                {
                    OrganizationId = vm.OrganizationId,
                    Name = vm.ChangeName,
                    CurrentPrice = vm.Price,
                    Categories = vm.SelectedCategories.Select(id => new BLLCategoryMinDTO(id)).ToList()
                };

                var result = await _bll.ChangeService.AddChangeAsync(changeDto);
                if (result == false) return BadRequest("Something went wrong while adding change");
            }

            return RedirectToAction("Organization", "Dashboard", new {Id = vm.OrganizationId});
        }

        
        [HttpGet("change/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _bll.ChangeService.GetChangeAsync(changeId: id.Value);
            if (change == null)
            {
                return NotFound();
            }

            var vm = new DeleteChangeViewModel()
            {
                Price = change.CurrentPrice,
                ChangeId = change.Id,
                OrganizationId = change.OrganizationId,
                ChangeName = change.Name
            };


            return View(vm);
        }
        
        
        [HttpPost("change/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteChangeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _bll.ChangeService.DeleteChangeAsync(vm.ChangeId);
                if (result == false) return BadRequest("Error while deleting entry");
                return RedirectToAction("Organization", "Dashboard", new {Id = vm.OrganizationId});
            }

            return BadRequest();
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _bll.ChangeService.GetChangeAsync(changeId: id.Value);
            if (change == null)
            {
                return NotFound();
            }

            var categories =
                (await _bll.OrganizationsService.GetOrganizationWithCategoriesAsync(change.OrganizationId)).Categories;
            
            var selectedCategoryIds = change.Categories.Select(dto => dto.Id).ToList();
            var vm = new EditChangeViewModel()
            {
                
                Price = change.CurrentPrice,
                ChangeId = change.Id,
                OrganizationId = change.OrganizationId,
                ChangeName = change.Name,
                Categories = categories.Select(dto =>
                    new SelectListItem(dto.CategoryName, dto.CategoryId.ToString(), selectedCategoryIds.Contains(dto.CategoryId)))
                    .ToList()
            };
            vm.Categories.Sort((item, item2) => int.Parse(item.Value).CompareTo(int.Parse(item2.Value)));
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditChangeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var input = new BLLChangeDTO()
                {
                    Id = vm.ChangeId,
                    OrganizationId = vm.OrganizationId,
                    CurrentPrice = vm.Price,
                    Name = vm.ChangeName,
                    Categories = vm.SelectedCategories.Select(i => new BLLCategoryMinDTO(i)).ToList()
                };

                var result = await _bll.ChangeService.EditChange(input);
                if (result == false) return BadRequest();
                return RedirectToAction("Organization", "Dashboard", new {Id = vm.OrganizationId});
            }
            return BadRequest();
        }
    }
}