using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.Product;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet("product/create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            var organization = await _bll.OrganizationsService.GetOrganizationWithCategoriesAsync(id);
            if (organization == null) return BadRequest();

            var vm = new CreateProductViewModel
            {
                OrganizationName = organization.Name,
                OrganizationId = organization.Id,
                Categories = organization.Categories
                    .Select(dto => new SelectListItem(dto.Name, dto.Id.ToString()))
                    .ToList()
            };
            return View(vm);
        }
        
        [HttpPost("product/create/{id}")]
        public async Task<IActionResult> Create( CreateProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var productDTO = new BLLProductDTO()
                {
                    OrganizationId = vm.OrganizationId,
                    ProductName = vm.ProductName,
                    CurrentPrice = vm.Price,
                    Categories = vm.SelectedCategories.Select(id => new BLLCategoryMinDTO(id)).ToList()
                };
                
                var result = await _bll.ProductService.AddProductAsync(productDTO);
                if (result == false) return BadRequest("Something went wrong while adding");
            }
            return RedirectToAction("Organization", "Dashboard", new {Id = vm.OrganizationId});
        }
        
        // GET: Products/Delete/5
        [HttpGet("product/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.ProductService.GetProductAsync(productId: id.Value);
            if (product == null)
            {
                return NotFound();
            }
            var vm = new DeleteProductViewModel()
            {
                Price = product.CurrentPrice,
                Description = "TODO: description",
                ProductId = product.Id,
                OrganizationId = product.OrganizationId,
                ProductName = product.ProductName
            };
            

            return View(vm);
        }

        // POST: Products/Delete/5
        [HttpPost("product/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _bll.ProductService.DeleteProductAsync(vm.ProductId);
                if (result == false) return BadRequest("Error while deleting entry");
                return RedirectToAction("Organization", "Dashboard", new {Id = vm.OrganizationId});
            }
            return BadRequest();
        }
    }
}