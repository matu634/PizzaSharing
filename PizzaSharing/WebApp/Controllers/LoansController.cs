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
    public class LoansController : Controller
    {
        private readonly AppDbContext _context;

        public LoansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Loans.Include(l => l.LoanGiver).Include(l => l.LoanTaker).Include(l => l.ReceiptParticipant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.LoanGiver)
                .Include(l => l.LoanTaker)
                .Include(l => l.ReceiptParticipant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["LoanGiverId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LoanTakerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ReceiptParticipantId"] = new SelectList(_context.ReceiptParticipants, "Id", "Id");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceiptParticipantId,IsPaid,LoanGiverId,LoanTakerId,Id")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoanGiverId"] = new SelectList(_context.Users, "Id", "Id", loan.LoanGiverId);
            ViewData["LoanTakerId"] = new SelectList(_context.Users, "Id", "Id", loan.LoanTakerId);
            ViewData["ReceiptParticipantId"] = new SelectList(_context.ReceiptParticipants, "Id", "Id", loan.ReceiptParticipantId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["LoanGiverId"] = new SelectList(_context.Users, "Id", "Id", loan.LoanGiverId);
            ViewData["LoanTakerId"] = new SelectList(_context.Users, "Id", "Id", loan.LoanTakerId);
            ViewData["ReceiptParticipantId"] = new SelectList(_context.ReceiptParticipants, "Id", "Id", loan.ReceiptParticipantId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceiptParticipantId,IsPaid,LoanGiverId,LoanTakerId,Id")] Loan loan)
        {
            if (id != loan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.Id))
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
            ViewData["LoanGiverId"] = new SelectList(_context.Users, "Id", "Id", loan.LoanGiverId);
            ViewData["LoanTakerId"] = new SelectList(_context.Users, "Id", "Id", loan.LoanTakerId);
            ViewData["ReceiptParticipantId"] = new SelectList(_context.ReceiptParticipants, "Id", "Id", loan.ReceiptParticipantId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.LoanGiver)
                .Include(l => l.LoanTaker)
                .Include(l => l.ReceiptParticipant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}