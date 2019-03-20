using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
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
        private readonly IAppUnitOfWork _uow;

        public ProductInCategoriesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ProductInCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInCategory>>> GetProductInCategories()
        {
            var productsInCategories = await _uow.ProductsInCategories.AllAsync();
            return Ok(productsInCategories);
        }

        // GET: api/ProductInCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInCategory>> GetProductInCategory(int id)
        {
            var productInCategory = await _uow.ProductsInCategories.FindAsync(id);

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

            if (await _uow.ProductsInCategories.FindAsync(id) == null ||
                await _uow.ProductsInCategories.FindAsync(productInCategory.Id) == null)
            {
                return NotFound();
            }

            _uow.ProductsInCategories.Update(productInCategory);
            await _uow.SaveChangesAsync();

            return Ok();
        }

        // POST: api/ProductInCategories
        [HttpPost]
        public async Task<ActionResult<ProductInCategory>> PostProductInCategory(ProductInCategory productInCategory)
        {
            await _uow.ProductsInCategories.AddAsync(productInCategory);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetProductInCategory", new {id = productInCategory.Id}, productInCategory);
        }

        // DELETE: api/ProductInCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductInCategory>> DeleteProductInCategory(int id)
        {
            var productInCategory = await _uow.ProductsInCategories.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            _uow.ProductsInCategories.Remove(productInCategory);
            await _uow.SaveChangesAsync();

            return productInCategory;
        }
    }
}