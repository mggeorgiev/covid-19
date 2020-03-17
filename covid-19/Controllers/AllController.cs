using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using covid_19.Data;
using covid_19.Models;

namespace covid_19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AllController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: api/All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<All>>> GetAll()
        {
            return await _context.All.ToListAsync();
        }


        // GET: api/All/5
        [HttpGet("{id}")]
        public async Task<ActionResult<All>> GetAll(int id)
        {
            var all = await _context.All.FindAsync(id);

            if (all == null)
            {
                return NotFound();
            }

            return all;
        }

        // PUT: api/All/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAll(int id, All all)
        {
            if (id != all.Id)
            {
                return BadRequest();
            }

            _context.Entry(all).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllExists(id))
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

        // POST: api/All
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<All>> PostAll(All all)
        {
            _context.All.Add(all);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = all.Id }, all);
        }

        // DELETE: api/All/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<All>> DeleteAll(int id)
        {
            var all = await _context.All.FindAsync(id);
            if (all == null)
            {
                return NotFound();
            }

            _context.All.Remove(all);
            await _context.SaveChangesAsync();

            return all;
        }

        private bool AllExists(int id)
        {
            return _context.All.Any(e => e.Id == id);
        }
    }
}
