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
    public class ReceiptsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ReceiptsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Receipts.AllAsync());
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _uow.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // GET: Receipts/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ReceiptViewModel
            {
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname))
            };
            return View(viewModel);
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsFinalized,CreatedTime,ReceiptManagerId,Id")]
            Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                await _uow.Receipts.AddAsync(receipt);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ReceiptViewModel
            {
                Receipt = receipt,
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname))
            };
            return View(viewModel);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _uow.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            var viewModel = new ReceiptViewModel
            {
                Receipt = receipt,
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname))
            };
            return View(viewModel);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IsFinalized,CreatedTime,ReceiptManagerId,Id")]
            Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Receipts.Update(receipt);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new ReceiptViewModel
            {
                Receipt = receipt,
                AppUsers = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), nameof(AppUser.Id),
                    nameof(AppUser.UserNickname))
            };
            return View(viewModel);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _uow.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.Receipts.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}