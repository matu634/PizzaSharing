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
    public class ReceiptParticipantsController : Controller
    {
        private readonly AppDbContext _context;

        public ReceiptParticipantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReceiptParticipants
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReceiptParticipants.Include(r => r.AppUser).Include(r => r.Receipt);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReceiptParticipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptParticipant = await _context.ReceiptParticipants
                .Include(r => r.AppUser)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptParticipant == null)
            {
                return NotFound();
            }

            return View(receiptParticipant);
        }

        // GET: ReceiptParticipants/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id");
            return View();
        }

        // POST: ReceiptParticipants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceiptId,AppUserId,Id")] ReceiptParticipant receiptParticipant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptParticipant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", receiptParticipant.AppUserId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptParticipant.ReceiptId);
            return View(receiptParticipant);
        }

        // GET: ReceiptParticipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptParticipant = await _context.ReceiptParticipants.FindAsync(id);
            if (receiptParticipant == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", receiptParticipant.AppUserId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptParticipant.ReceiptId);
            return View(receiptParticipant);
        }

        // POST: ReceiptParticipants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceiptId,AppUserId,Id")] ReceiptParticipant receiptParticipant)
        {
            if (id != receiptParticipant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptParticipant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptParticipantExists(receiptParticipant.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", receiptParticipant.AppUserId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptParticipant.ReceiptId);
            return View(receiptParticipant);
        }

        // GET: ReceiptParticipants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptParticipant = await _context.ReceiptParticipants
                .Include(r => r.AppUser)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptParticipant == null)
            {
                return NotFound();
            }

            return View(receiptParticipant);
        }

        // POST: ReceiptParticipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiptParticipant = await _context.ReceiptParticipants.FindAsync(id);
            _context.ReceiptParticipants.Remove(receiptParticipant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptParticipantExists(int id)
        {
            return _context.ReceiptParticipants.Any(e => e.Id == id);
        }
    }
}
