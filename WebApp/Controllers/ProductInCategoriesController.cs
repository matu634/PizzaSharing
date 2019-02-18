using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Controllers
{
    public class ProductInCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public ProductInCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductInCategories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductInCategories.Include(p => p.Category).Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductInCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _context.ProductInCategories
                .Include(p => p.Category)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductInCategoryId == id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            return View(productInCategory);
        }

        // GET: ProductInCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
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
                _context.Add(productInCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", productInCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productInCategory.ProductId);
            return View(productInCategory);
        }

        // GET: ProductInCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _context.ProductInCategories.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", productInCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productInCategory.ProductId);
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
                try
                {
                    _context.Update(productInCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInCategoryExists(productInCategory.ProductInCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", productInCategory.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productInCategory.ProductId);
            return View(productInCategory);
        }

        // GET: ProductInCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _context.ProductInCategories
                .Include(p => p.Category)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductInCategoryId == id);
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
            var productInCategory = await _context.ProductInCategories.FindAsync(id);
            _context.ProductInCategories.Remove(productInCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInCategoryExists(int id)
        {
            return _context.ProductInCategories.Any(e => e.ProductInCategoryId == id);
        }
    }
}
