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
    public class LoanRowsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoanRowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LoanRows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanRow>>> GetLoanRows()
        {
            return await _context.LoanRows.ToListAsync();
        }

        // GET: api/LoanRows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanRow>> GetLoanRow(int id)
        {
            var loanRow = await _context.LoanRows.FindAsync(id);

            if (loanRow == null)
            {
                return NotFound();
            }

            return loanRow;
        }

        // PUT: api/LoanRows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanRow(int id, LoanRow loanRow)
        {
            if (id != loanRow.Id)
            {
                return BadRequest();
            }

            _context.Entry(loanRow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanRowExists(id))
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

        // POST: api/LoanRows
        [HttpPost]
        public async Task<ActionResult<LoanRow>> PostLoanRow(LoanRow loanRow)
        {
            _context.LoanRows.Add(loanRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoanRow", new { id = loanRow.Id }, loanRow);
        }

        // DELETE: api/LoanRows/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoanRow>> DeleteLoanRow(int id)
        {
            var loanRow = await _context.LoanRows.FindAsync(id);
            if (loanRow == null)
            {
                return NotFound();
            }

            _context.LoanRows.Remove(loanRow);
            await _context.SaveChangesAsync();

            return loanRow;
        }

        private bool LoanRowExists(int id)
        {
            return _context.LoanRows.Any(e => e.Id == id);
        }
    }
}
