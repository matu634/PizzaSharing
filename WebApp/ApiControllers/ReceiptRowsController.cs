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
    public class ReceiptRowsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReceiptRowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptRows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptRow>>> GetReceiptRows()
        {
            return await _context.ReceiptRows.ToListAsync();
        }

        // GET: api/ReceiptRows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptRow>> GetReceiptRow(int id)
        {
            var receiptRow = await _context.ReceiptRows.FindAsync(id);

            if (receiptRow == null)
            {
                return NotFound();
            }

            return receiptRow;
        }

        // PUT: api/ReceiptRows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptRow(int id, ReceiptRow receiptRow)
        {
            if (id != receiptRow.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptRow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptRowExists(id))
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

        // POST: api/ReceiptRows
        [HttpPost]
        public async Task<ActionResult<ReceiptRow>> PostReceiptRow(ReceiptRow receiptRow)
        {
            _context.ReceiptRows.Add(receiptRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiptRow", new { id = receiptRow.Id }, receiptRow);
        }

        // DELETE: api/ReceiptRows/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReceiptRow>> DeleteReceiptRow(int id)
        {
            var receiptRow = await _context.ReceiptRows.FindAsync(id);
            if (receiptRow == null)
            {
                return NotFound();
            }

            _context.ReceiptRows.Remove(receiptRow);
            await _context.SaveChangesAsync();

            return receiptRow;
        }

        private bool ReceiptRowExists(int id)
        {
            return _context.ReceiptRows.Any(e => e.Id == id);
        }
    }
}
