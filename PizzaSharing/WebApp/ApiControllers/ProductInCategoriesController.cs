using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductInCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductInCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInCategory>>> GetProductInCategories()
        {
            return await _context.ProductInCategories.ToListAsync();
        }

        // GET: api/ProductInCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInCategory>> GetProductInCategory(int id)
        {
            var productInCategory = await _context.ProductInCategories.FindAsync(id);

            if (productInCategory == null)
            {
                return NotFound();
            }

            return productInCategory;
        }

        // PUT: api/ProductInCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInCategory(int id, ProductInCategory productInCategory)
        {
            if (id != productInCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(productInCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductInCategories
        [HttpPost]
        public async Task<ActionResult<ProductInCategory>> PostProductInCategory(ProductInCategory productInCategory)
        {
            _context.ProductInCategories.Add(productInCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductInCategory", new { id = productInCategory.Id }, productInCategory);
        }

        // DELETE: api/ProductInCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductInCategory>> DeleteProductInCategory(int id)
        {
            var productInCategory = await _context.ProductInCategories.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            _context.ProductInCategories.Remove(productInCategory);
            await _context.SaveChangesAsync();

            return productInCategory;
        }

        private bool ProductInCategoryExists(int id)
        {
            return _context.ProductInCategories.Any(e => e.Id == id);
        }
    }
}
