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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IAppUnitOfWork _uow;


        public CategoriesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _uow.Categories.AllAsync();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _uow.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CategoryViewModel
            {
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,OrganizationId")]
            Category category)
        {
            if (ModelState.IsValid)
            {
                await _uow.Categories.AddAsync(category);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new CategoryViewModel
            {
                Category = category,
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _uow.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Category = category,
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,OrganizationId")]
            Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Categories.Update(category);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new CategoryViewModel
            {
                Category = category,
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };
            return View(viewModel);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _uow.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.Categories.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}