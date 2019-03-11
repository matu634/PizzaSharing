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
    public class ChangesController : Controller
    {
        private readonly AppDbContext _context;

        public ChangesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Changes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Changes.Include(c => c.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Changes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _context.Changes
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (change == null)
            {
                return NotFound();
            }

            return View(change);
        }

        // GET: Changes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Changes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChangeName,CategoryId,Id")] Change change)
        {
            if (ModelState.IsValid)
            {
                _context.Add(change);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", change.CategoryId);
            return View(change);
        }

        // GET: Changes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _context.Changes.FindAsync(id);
            if (change == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", change.CategoryId);
            return View(change);
        }

        // POST: Changes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChangeName,CategoryId,Id")] Change change)
        {
            if (id != change.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(change);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChangeExists(change.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", change.CategoryId);
            return View(change);
        }

        // GET: Changes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _context.Changes
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (change == null)
            {
                return NotFound();
            }

            return View(change);
        }

        // POST: Changes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var change = await _context.Changes.FindAsync(id);
            _context.Changes.Remove(change);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChangeExists(int id)
        {
            return _context.Changes.Any(e => e.Id == id);
        }
    }
}
