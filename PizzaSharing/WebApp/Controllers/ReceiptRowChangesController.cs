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
    public class ReceiptRowChangesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ReceiptRowChangesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ReceiptRowChanges
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ReceiptRowChanges.AllAsync());
        }

        // GET: ReceiptRowChanges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRowChange = await _uow.ReceiptRowChanges.FindAsync(id);
            if (receiptRowChange == null)
            {
                return NotFound();
            }

            return View(receiptRowChange);
        }

        // GET: ReceiptRowChanges/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ReceiptRowChangeViewModel
            {
                Changes = new SelectList(await _uow.Changes.AllAsync(), nameof(Change.Id), nameof(Change.ChangeName)),
                Rows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id), nameof(ReceiptRow.Id))
            };
            
            return View(viewModel);
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
                await _uow.ReceiptRowChanges.AddAsync(receiptRowChange);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new ReceiptRowChangeViewModel
            {
                RowChange = receiptRowChange,
                Changes = new SelectList(await _uow.Changes.AllAsync(), nameof(Change.Id), nameof(Change.ChangeName)),
                Rows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id), nameof(ReceiptRow.Id))
            };
            
            return View(viewModel);
        }

        // GET: ReceiptRowChanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRowChange = await _uow.ReceiptRowChanges.FindAsync(id);
            if (receiptRowChange == null)
            {
                return NotFound();
            }
            var viewModel = new ReceiptRowChangeViewModel
            {
                RowChange = receiptRowChange,
                Changes = new SelectList(await _uow.Changes.AllAsync(), nameof(Change.Id), nameof(Change.ChangeName)),
                Rows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id), nameof(ReceiptRow.Id))
            };
            
            return View(viewModel);
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
                _uow.ReceiptRowChanges.Update(receiptRowChange);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new ReceiptRowChangeViewModel
            {
                RowChange = receiptRowChange,
                Changes = new SelectList(await _uow.Changes.AllAsync(), nameof(Change.Id), nameof(Change.ChangeName)),
                Rows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id), nameof(ReceiptRow.Id))
            };
            
            return View(viewModel);
        }

        // GET: ReceiptRowChanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptRowChange = await _uow.ReceiptRowChanges.FindAsync(id);
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
            _uow.ReceiptRowChanges.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
