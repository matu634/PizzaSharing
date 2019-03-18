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
    public class LoanRowsController : Controller
    {
        private readonly AppDbContext _context;

        public LoanRowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LoanRows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.LoanRows.Include(l => l.Loan).Include(l => l.ReceiptRow);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LoanRows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRow = await _context.LoanRows
                .Include(l => l.Loan)
                .Include(l => l.ReceiptRow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanRow == null)
            {
                return NotFound();
            }

            return View(loanRow);
        }

        // GET: LoanRows/Create
        public IActionResult Create()
        {
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "Id");
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id");
            return View();
        }

        // POST: LoanRows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsPaid,LoanId,ReceiptRowId,Involvement,Id")] LoanRow loanRow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanRow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "Id", loanRow.LoanId);
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id", loanRow.ReceiptRowId);
            return View(loanRow);
        }

        // GET: LoanRows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRow = await _context.LoanRows.FindAsync(id);
            if (loanRow == null)
            {
                return NotFound();
            }
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "Id", loanRow.LoanId);
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id", loanRow.ReceiptRowId);
            return View(loanRow);
        }

        // POST: LoanRows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IsPaid,LoanId,ReceiptRowId,Involvement,Id")] LoanRow loanRow)
        {
            if (id != loanRow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanRow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanRowExists(loanRow.Id))
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
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "Id", loanRow.LoanId);
            ViewData["ReceiptRowId"] = new SelectList(_context.ReceiptRows, "Id", "Id", loanRow.ReceiptRowId);
            return View(loanRow);
        }

        // GET: LoanRows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRow = await _context.LoanRows
                .Include(l => l.Loan)
                .Include(l => l.ReceiptRow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanRow == null)
            {
                return NotFound();
            }

            return View(loanRow);
        }

        // POST: LoanRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanRow = await _context.LoanRows.FindAsync(id);
            _context.LoanRows.Remove(loanRow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanRowExists(int id)
        {
            return _context.LoanRows.Any(e => e.Id == id);
        }
    }
}
