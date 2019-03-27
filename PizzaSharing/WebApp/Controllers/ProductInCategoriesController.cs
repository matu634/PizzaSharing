using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.App.EF;
using Domain;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductInCategoriesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductInCategoriesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductInCategories
        public async Task<IActionResult> Index()
        {
            var productsInCategories = await _uow.ProductsInCategories.AllAsync();
            return View(productsInCategories);
        }

        // GET: ProductInCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _uow.ProductsInCategories.FindAsync(id);

            if (productInCategory == null)
            {
                return NotFound();
            }

            return View(productInCategory);
        }

        // GET: ProductInCategories/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductInCategoryViewModel
            {
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryAndOwnerNAme)),
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName))
            };
            return View(viewModel);
        }

        // POST: ProductInCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,ProductId")] ProductInCategory productInCategory)
        {
            if (ModelState.IsValid)
            {
                await _uow.ProductsInCategories.AddAsync(productInCategory);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProductInCategoryViewModel
            {
                ProductInCategory = productInCategory,
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryAndOwnerNAme)),
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName))
            };
            return View(viewModel);
        }

        // GET: ProductInCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _uow.ProductsInCategories.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            var viewModel = new ProductInCategoryViewModel
            {
                ProductInCategory = productInCategory,
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryAndOwnerNAme)),
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName))
            };
            return View(viewModel);
        }

        // POST: ProductInCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,CategoryId,ProductId")] ProductInCategory productInCategory)
        {
            if (id != productInCategory.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                _uow.ProductsInCategories.Update(productInCategory);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProductInCategoryViewModel
            {
                ProductInCategory = productInCategory,
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryAndOwnerNAme)),
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName))
            };
            return View(viewModel);
        }

        // GET: ProductInCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _uow.ProductsInCategories.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            return View(productInCategory);
        }

        // POST: ProductInCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.ProductsInCategories.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}