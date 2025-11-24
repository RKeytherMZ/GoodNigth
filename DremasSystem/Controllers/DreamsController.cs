using DremasSystem.Data;
using DremasSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DremasSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DreamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DreamsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Dream dream)
        {
            _context.Dreams.Add(dream);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = dream.Id }, dream);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dream = await _context.Dreams.FindAsync(id);

            if (dream == null)
                return NotFound();

            return Ok(dream);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dreams = await _context.Dreams.ToListAsync();
            return Ok(dreams);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Dream dream)
        {
            if (id != dream.Id)
                return BadRequest();

            _context.Entry(dream).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dreams.Any(d => d.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }



    }
}
