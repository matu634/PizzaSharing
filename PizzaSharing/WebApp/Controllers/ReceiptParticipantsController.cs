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
    public class ReceiptParticipantsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ReceiptParticipantsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ReceiptParticipants
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ReceiptParticipants.AllAsync());
        }

        // GET: ReceiptParticipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptParticipant = await _uow.ReceiptParticipants.FindAsync(id);
            if (receiptParticipant == null)
            {
                return NotFound();
            }

            return View(receiptParticipant);
        }

        // GET: ReceiptParticipants/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ReceiptParticipantViewModel
            {
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
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
                await _uow.ReceiptParticipants.AddAsync(receiptParticipant);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ReceiptParticipantViewModel
            {
                ReceiptParticipant = receiptParticipant,
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
        }

        // GET: ReceiptParticipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptParticipant = await _uow.ReceiptParticipants.FindAsync(id);
            if (receiptParticipant == null)
            {
                return NotFound();
            }

            var viewModel = new ReceiptParticipantViewModel
            {
                ReceiptParticipant = receiptParticipant,
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
        }

        // POST: ReceiptParticipants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ReceiptId,AppUserId,Id")] ReceiptParticipant receiptParticipant)
        {
            if (id != receiptParticipant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.ReceiptParticipants.Update(receiptParticipant);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ReceiptParticipantViewModel
            {
                ReceiptParticipant = receiptParticipant,
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname)),
                Receipts = new SelectList(await _uow.Receipts.AllAsync(), nameof(Receipt.Id), nameof(Receipt.Id))
            };
            return View(viewModel);
        }

        // GET: ReceiptParticipants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptParticipant = await _uow.ReceiptParticipants.FindAsync(id);
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
            _uow.ReceiptParticipants.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}