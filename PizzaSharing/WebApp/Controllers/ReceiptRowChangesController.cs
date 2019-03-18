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
    public class ReceiptRowChangesController : Controller
    {
        private readonly AppDbContext _context;

        public ReceiptRowChangesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReceiptRowChanges
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReceiptRowChanges.Include(r => r.Change).Include(r => r.ReceiptRow);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReceiptRowChanges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRowChange = await _context.ReceiptRowChanges
                .Include(r => r.Change)
                .Include(r => r.ReceiptRow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptRowChange == null)
            {
                return NotFound();
            }

            return View(receiptRowChange);
        }

        // GET: ReceiptRowChanges/Create
        public IActionResult Create()
        {
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName");
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id");
            return View();
        }

        // POST: ReceiptRowChanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceiptRowId,ChangeId,Id")] ReceiptRowChange receiptRowChange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptRowChange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName", receiptRowChange.ChangeId);
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id", receiptRowChange.ReceiptRowId);
            return View(receiptRowChange);
        }

        // GET: ReceiptRowChanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRowChange = await _context.ReceiptRowChanges.FindAsync(id);
            if (receiptRowChange == null)
            {
                return NotFound();
            }
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName", receiptRowChange.ChangeId);
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id", receiptRowChange.ReceiptRowId);
            return View(receiptRowChange);
        }

        // POST: ReceiptRowChanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceiptRowId,ChangeId,Id")] ReceiptRowChange receiptRowChange)
        {
            if (id != receiptRowChange.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptRowChange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptRowChangeExists(receiptRowChange.Id))
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
            ViewData["ChangeId"] = new SelectList(_context.Changes, "Id", "ChangeName", receiptRowChange.ChangeId);
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id", receiptRowChange.ReceiptRowId);
            return View(receiptRowChange);
        }

        // GET: ReceiptRowChanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRowChange = await _context.ReceiptRowChanges
                .Include(r => r.Change)
                .Include(r => r.ReceiptRow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptRowChange == null)
            {
                return NotFound();
            }

            return View(receiptRowChange);
        }

        // POST: ReceiptRowChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiptRowChange = await _context.ReceiptRowChanges.FindAsync(id);
            _context.ReceiptRowChanges.Remove(receiptRowChange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptRowChangeExists(int id)
        {
            return _context.ReceiptRowChanges.Any(e => e.Id == id);
        }
    }
}
