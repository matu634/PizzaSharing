using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ReceiptRowsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ReceiptRowsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ReceiptRows
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ReceiptRows.AllAsync());
        }

        // GET: ReceiptRows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRow = await _uow.ReceiptRows.FindAsync(id);
            if (receiptRow == null)
            {
                return NotFound();
            }

            return View(receiptRow);
        }

        // GET: ReceiptRows/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ReceiptRowViewModel
            {
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
        }

        // POST: ReceiptRows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,RowDiscount,ProductId,ReceiptId,Id")]
            ReceiptRow receiptRow)
        {
            if (ModelState.IsValid)
            {
                await _uow.ReceiptRows.AddAsync(receiptRow);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ReceiptRowViewModel
            {
                ReceiptRow = receiptRow,
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
        }

        // GET: ReceiptRows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRow = await _uow.ReceiptRows.FindAsync(id);
            if (receiptRow == null)
            {
                return NotFound();
            }

            var viewModel = new ReceiptRowViewModel
            {
                ReceiptRow = receiptRow,
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
        }

        // POST: ReceiptRows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Amount,RowDiscount,ProductId,ReceiptId,Id")]
            ReceiptRow receiptRow)
        {
            if (id != receiptRow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.ReceiptRows.Update(receiptRow);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ReceiptRowViewModel
            {
                ReceiptRow = receiptRow,
                Products = new SelectList(await _uow.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
        }

        // GET: ReceiptRows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRow = await _uow.ReceiptRows.FindAsync(id);
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
            _uow.ReceiptRows.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}