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
    public class LoanRowsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public LoanRowsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: LoanRows
        public async Task<IActionResult> Index()
        {
            return View(await _uow.LoanRows.AllAsync());
        }

        // GET: LoanRows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRow = await _uow.LoanRows.FindAsync(id);
            if (loanRow == null)
            {
                return NotFound();
            }

            return View(loanRow);
        }

        // GET: LoanRows/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new LoanRowViewModel
            {
                Loans = new SelectList(await _uow.Loans.AllAsync(), nameof(Loan.Id), nameof(Loan.Id)),
                ReceiptRows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id),
                    nameof(ReceiptRow.Id))
            };
            return View(viewModel);
        }

        // POST: LoanRows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsPaid,LoanId,ReceiptRowId,Involvement,Id")]
            LoanRow loanRow)
        {
            if (ModelState.IsValid)
            {
                await _uow.LoanRows.AddAsync(loanRow);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new LoanRowViewModel
            {
                LoanRow = loanRow,
                Loans = new SelectList(await _uow.Loans.AllAsync(), nameof(Loan.Id), nameof(Loan.Id)),
                ReceiptRows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id),
                    nameof(ReceiptRow.Id))
            };
            return View(viewModel);
        }

        // GET: LoanRows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRow = await _uow.LoanRows.FindAsync(id);
            if (loanRow == null)
            {
                return NotFound();
            }

            var viewModel = new LoanRowViewModel
            {
                LoanRow = loanRow,
                Loans = new SelectList(await _uow.Loans.AllAsync(), nameof(Loan.Id), nameof(Loan.Id)),
                ReceiptRows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id),
                    nameof(ReceiptRow.Id))
            };
            return View(viewModel);
        }

        // POST: LoanRows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IsPaid,LoanId,ReceiptRowId,Involvement,Id")]
            LoanRow loanRow)
        {
            if (id != loanRow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.LoanRows.Update(loanRow);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var viewModel = new LoanRowViewModel
            {
                LoanRow = loanRow,
                Loans = new SelectList(await _uow.Loans.AllAsync(), nameof(Loan.Id), nameof(Loan.Id)),
                ReceiptRows = new SelectList(await _uow.ReceiptRows.AllAsync(), nameof(ReceiptRow.Id),
                    nameof(ReceiptRow.Id))
            };
            return View(viewModel);
        }

        // GET: LoanRows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanRow = await _uow.LoanRows.FindAsync(id);
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
            _uow.LoanRows.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}