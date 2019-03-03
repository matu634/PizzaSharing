using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class PricesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PricesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: Prices
        public async Task<IActionResult> Index()
        {
//            var appDbContext = _context.Prices.Include(p => p.Change).Include(p => p.Product);
//            return View(await appDbContext.ToListAsync());
            var prices =  await _uow.Prices.AllAsync();
            return View(prices);
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var price = await _uow.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ChangeId"] = new SelectList(await _uow.Changes.AllAsync(), "ChangeId", "ChangeName").Prepend(new SelectListItem{Text = "Select change", Value = ""});
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName").Prepend(new SelectListItem{Text = "Select product", Value = ""});
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriceId,Value,ProductId,ChangeId,ValidFrom,ValidTo")] Price price)
        {
            if (ModelState.IsValid)
            {
                await _uow.Prices.AddAsync(price);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChangeId"] = new SelectList(await _uow.Changes.AllAsync(), "ChangeId", "ChangeName", price.ChangeId).Prepend(new SelectListItem{Text = "Select change", Value = ""});
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName", price.ProductId).Prepend(new SelectListItem{Text = "Select product", Value = ""});
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["ChangeId"] = new SelectList(await _uow.Changes.AllAsync(), "ChangeId", "ChangeName", price.ChangeId).Prepend(new SelectListItem{Text = "Select change", Value = ""});
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName", price.ProductId).Prepend(new SelectListItem{Text = "Select product", Value = ""});
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriceId,Value,ProductId,ChangeId,ValidFrom,ValidTo")] Price price)
        {
            if (id != price.PriceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Prices.Update(price);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChangeId"] = new SelectList(await _uow.Changes.AllAsync(), "ChangeId", "ChangeName", price.ChangeId).Prepend(new SelectListItem{Text = "Select change", Value = ""});
            ViewData["ProductId"] = new SelectList(await _uow.Products.AllAsync(), "ProductId", "ProductName", price.ProductId).Prepend(new SelectListItem{Text = "Select product", Value = ""});
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _uow.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.Prices.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
