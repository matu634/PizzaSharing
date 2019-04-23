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
using WebApp.Areas.Admin.ViewModels;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Products.AllAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductViewModel
            {
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _uow.Products.AddAsync(product);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProductViewModel
            {
                Product = product,
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                Product = product,
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Products.Update(product);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProductViewModel
            {
                Product = product,
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _uow.Products.FindAsync(id);
            _uow.Products.Remove(product);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}