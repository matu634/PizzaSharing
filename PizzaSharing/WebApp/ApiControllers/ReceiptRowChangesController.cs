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
    public class ReceiptRowChangesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReceiptRowChangesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptRowChanges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptRowChange>>> GetReceiptRowChanges()
        {
            return await _context.ReceiptRowChanges.ToListAsync();
        }

        // GET: api/ReceiptRowChanges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptRowChange>> GetReceiptRowChange(int id)
        {
            var receiptRowChange = await _context.ReceiptRowChanges.FindAsync(id);

            if (receiptRowChange == null)
            {
                return NotFound();
            }

            return receiptRowChange;
        }

        // PUT: api/ReceiptRowChanges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptRowChange(int id, ReceiptRowChange receiptRowChange)
        {
            if (id != receiptRowChange.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptRowChange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptRowChangeExists(id))
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

        // POST: api/ReceiptRowChanges
        [HttpPost]
        public async Task<ActionResult<ReceiptRowChange>> PostReceiptRowChange(ReceiptRowChange receiptRowChange)
        {
            _context.ReceiptRowChanges.Add(receiptRowChange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiptRowChange", new { id = receiptRowChange.Id }, receiptRowChange);
        }

        // DELETE: api/ReceiptRowChanges/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReceiptRowChange>> DeleteReceiptRowChange(int id)
        {
            var receiptRowChange = await _context.ReceiptRowChanges.FindAsync(id);
            if (receiptRowChange == null)
            {
                return NotFound();
            }

            _context.ReceiptRowChanges.Remove(receiptRowChange);
            await _context.SaveChangesAsync();

            return receiptRowChange;
        }

        private bool ReceiptRowChangeExists(int id)
        {
            return _context.ReceiptRowChanges.Any(e => e.Id == id);
        }
    }
}
