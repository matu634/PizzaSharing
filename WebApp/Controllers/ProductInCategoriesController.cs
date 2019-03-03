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
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "CategoryId", "CategoryName");
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName");
            return View();
        }

        // POST: ProductInCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductInCategoryId,CategoryId,ProductId")] ProductInCategory productInCategory)
        {
            if (ModelState.IsValid)
            {
                await _uow.ProductsInCategories.AddAsync(productInCategory);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "CategoryId", "CategoryName", productInCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName", productInCategory.ProductId);
            return View(productInCategory);
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
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "CategoryId", "CategoryName", productInCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName", productInCategory.ProductId);
            return View(productInCategory);
        }

        // POST: ProductInCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductInCategoryId,CategoryId,ProductId")] ProductInCategory productInCategory)
        {
            if (id != productInCategory.ProductInCategoryId)
            {
                return NotFound();
            }

            
            if (ModelState.IsValid)
            {
                _uow.ProductsInCategories.Update(productInCategory);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "CategoryId", "CategoryName", productInCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName", productInCategory.ProductId);
            return View(productInCategory);
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
