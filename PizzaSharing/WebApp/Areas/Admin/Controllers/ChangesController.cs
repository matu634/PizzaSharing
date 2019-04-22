using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Area("Admin")]
    public class ChangesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ChangesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Changes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Changes.AllAsync());
        }

        // GET: Changes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _uow.Changes.FindAsync(id);
            if (change == null)
            {
                return NotFound();
            }

            return View(change);
        }

        // GET: Changes/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ChangeViewModel
            {
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryName)),
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };

            return View(viewModel);
        }

        // POST: Changes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Change change)
        {
            if (ModelState.IsValid)
            {
                await _uow.Changes.AddAsync(change);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ChangeViewModel
            {
                Change = change,
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryName)),
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };

            return View(viewModel);
        }

        // GET: Changes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _uow.Changes.FindAsync(id);
            if (change == null)
            {
                return NotFound();
            }

            var viewModel = new ChangeViewModel
            {
                Change = change,
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryName)),
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };

            return View(viewModel);
        }

        // POST: Changes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] Change change)
        {
            if (id != change.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Changes.Update(change);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ChangeViewModel
            {
                Change = change,
                Categories = new SelectList(await _uow.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryName)),
                Organizations = new SelectList(await _uow.Organizations.AllAsync(), nameof(Organization.Id),
                    nameof(Organization.OrganizationName))
            };

            return View(viewModel);
        }

        // GET: Changes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var change = await _uow.Changes.FindAsync(id);
            if (change == null)
            {
                return NotFound();
            }

            return View(change);
        }

        // POST: Changes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.Changes.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}