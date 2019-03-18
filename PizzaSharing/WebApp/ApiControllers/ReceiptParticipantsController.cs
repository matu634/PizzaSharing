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
    public class ReceiptParticipantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReceiptParticipantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptParticipants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptParticipant>>> GetReceiptParticipants()
        {
            return await _context.ReceiptParticipants.ToListAsync();
        }

        // GET: api/ReceiptParticipants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptParticipant>> GetReceiptParticipant(int id)
        {
            var receiptParticipant = await _context.ReceiptParticipants.FindAsync(id);

            if (receiptParticipant == null)
            {
                return NotFound();
            }

            return receiptParticipant;
        }

        // PUT: api/ReceiptParticipants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptParticipant(int id, ReceiptParticipant receiptParticipant)
        {
            if (id != receiptParticipant.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptParticipant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptParticipantExists(id))
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

        // POST: api/ReceiptParticipants
        [HttpPost]
        public async Task<ActionResult<ReceiptParticipant>> PostReceiptParticipant(ReceiptParticipant receiptParticipant)
        {
            _context.ReceiptParticipants.Add(receiptParticipant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiptParticipant", new { id = receiptParticipant.Id }, receiptParticipant);
        }

        // DELETE: api/ReceiptParticipants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReceiptParticipant>> DeleteReceiptParticipant(int id)
        {
            var receiptParticipant = await _context.ReceiptParticipants.FindAsync(id);
            if (receiptParticipant == null)
            {
                return NotFound();
            }

            _context.ReceiptParticipants.Remove(receiptParticipant);
            await _context.SaveChangesAsync();

            return receiptParticipant;
        }

        private bool ReceiptParticipantExists(int id)
        {
            return _context.ReceiptParticipants.Any(e => e.Id == id);
        }
    }
}
