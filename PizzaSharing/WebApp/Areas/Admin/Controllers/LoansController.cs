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
using Domain.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Area("Admin")]
    public class LoansController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public LoansController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Loans.AllAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _uow.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new LoanViewModel
            {
                LoanGivers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname)),
                LoanTakers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname)),
                ReceiptParticipants = new SelectList(await _uow.ReceiptParticipants.AllAsync(),
                    nameof(ReceiptParticipant.Id), nameof(ReceiptParticipant.Id))
            };
            return View(viewModel);
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceiptParticipantId,IsPaid,LoanGiverId,LoanTakerId,Id")]
            Loan loan)
        {
            if (ModelState.IsValid)
            {
                await _uow.Loans.AddAsync(loan);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new LoanViewModel
            {
                Loan = loan,
                LoanGivers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname), loan.LoanGiverId),
                LoanTakers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname), loan.LoanTakerId),
                ReceiptParticipants = new SelectList(await _uow.ReceiptParticipants.AllAsync(),
                    nameof(ReceiptParticipant.Id), nameof(ReceiptParticipant.Id), loan.ReceiptParticipantId)
            };
            return View(viewModel);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _uow.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            var viewModel = new LoanViewModel
            {
                Loan = loan,
                LoanGivers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname), loan.LoanGiverId),
                LoanTakers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname), loan.LoanTakerId),
                ReceiptParticipants = new SelectList(await _uow.ReceiptParticipants.AllAsync(),
                    nameof(ReceiptParticipant.Id), nameof(ReceiptParticipant.Id), loan.ReceiptParticipantId)
            };
            return View(viewModel);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceiptParticipantId,IsPaid,LoanGiverId,LoanTakerId,Id")]
            Loan loan)
        {
            if (id != loan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Loans.Update(loan);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var viewModel = new LoanViewModel
            {
                Loan = loan,
                LoanGivers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname), loan.LoanGiverId),
                LoanTakers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname), loan.LoanTakerId),
                ReceiptParticipants = new SelectList(await _uow.ReceiptParticipants.AllAsync(),
                    nameof(ReceiptParticipant.Id), nameof(ReceiptParticipant.Id), loan.ReceiptParticipantId)
            };
            return View(viewModel);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _uow.Loans.FindAsync(id);
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
            _uow.Loans.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}