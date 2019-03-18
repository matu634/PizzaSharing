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
    public class ReceiptRowsController : Controller
    {
        private readonly AppDbContext _context;

        public ReceiptRowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReceiptRows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReceiptRows.Include(r => r.Product).Include(r => r.Receipt);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReceiptRows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRow = await _context.ReceiptRows
                .Include(r => r.Product)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptRow == null)
            {
                return NotFound();
            }

            return View(receiptRow);
        }

        // GET: ReceiptRows/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName");
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id");
            return View();
        }

        // POST: ReceiptRows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,RowDiscount,ProductId,ReceiptId,Id")] ReceiptRow receiptRow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptRow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName", receiptRow.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptRow.ReceiptId);
            return View(receiptRow);
        }

        // GET: ReceiptRows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRow = await _context.ReceiptRows.FindAsync(id);
            if (receiptRow == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName", receiptRow.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptRow.ReceiptId);
            return View(receiptRow);
        }

        // POST: ReceiptRows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Amount,RowDiscount,ProductId,ReceiptId,Id")] ReceiptRow receiptRow)
        {
            if (id != receiptRow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptRow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptRowExists(receiptRow.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName", receiptRow.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptRow.ReceiptId);
            return View(receiptRow);
        }

        // GET: ReceiptRows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRow = await _context.ReceiptRows
                .Include(r => r.Product)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptRow == null)
            {
                return NotFound();
            }

            return View(receiptRow);
        }

        // POST: ReceiptRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiptRow = await _context.ReceiptRows.FindAsync(id);
            _context.ReceiptRows.Remove(receiptRow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptRowExists(int id)
        {
            return _context.ReceiptRows.Any(e => e.Id == id);
        }
    }
}
