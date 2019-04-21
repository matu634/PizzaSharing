using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class ChangeInCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public ChangeInCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChangeInCategories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ChangeInCategories.Include(c => c.Category).Include(c => c.Change);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ChangeInCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeInCategory = await _context.ChangeInCategories
                .Include(c => c.Category)
                .Include(c => c.Change)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (changeInCategory == null)
            {
                return NotFound();
            }

            return View(changeInCategory);
        }

        // GET: ChangeInCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName");
            return View();
        }

        // POST: ChangeInCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ChangeId,Id")] ChangeInCategory changeInCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(changeInCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", changeInCategory.CategoryId);
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName", changeInCategory.ChangeId);
            return View(changeInCategory);
        }

        // GET: ChangeInCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeInCategory = await _context.ChangeInCategories.FindAsync(id);
            if (changeInCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", changeInCategory.CategoryId);
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName", changeInCategory.ChangeId);
            return View(changeInCategory);
        }

        // POST: ChangeInCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ChangeId,Id")] ChangeInCategory changeInCategory)
        {
            if (id != changeInCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(changeInCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChangeInCategoryExists(changeInCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", changeInCategory.CategoryId);
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName", changeInCategory.ChangeId);
            return View(changeInCategory);
        }

        // GET: ChangeInCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeInCategory = await _context.ChangeInCategories
                .Include(c => c.Category)
                .Include(c => c.Change)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (changeInCategory == null)
            {
                return NotFound();
            }

            return View(changeInCategory);
        }

        // POST: ChangeInCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var changeInCategory = await _context.ChangeInCategories.FindAsync(id);
            _context.ChangeInCategories.Remove(changeInCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChangeInCategoryExists(int id)
        {
            return _context.ChangeInCategories.Any(e => e.Id == id);
        }
    }
}
