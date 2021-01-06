using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rest_deel2.Data;
using Rest_deel2.Model;

namespace Rest_deel2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsMessagesController : ControllerBase
    {
        private readonly NewsContext _context;

        public NewsMessagesController(NewsContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Get all the messages
        /// </summary>
        /// <returns>NewsMessage[]</returns>
        // GET: api/NewsMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsMessage>>> GetNewsMessage()
        {
            return await _context.NewsMessage.ToListAsync();
        }

        // GET: api/NewsMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsMessage>> GetNewsMessage(int? id)
        {
            var newsMessage = await _context.NewsMessage.FindAsync(id);

            if (newsMessage == null)
            {
                return NotFound();
            }

            return newsMessage;
        }

        // PUT: api/NewsMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsMessage(int? id, NewsMessage newsMessage)
        {
            if (id != newsMessage.id)
            {
                return BadRequest();
            }

            _context.Entry(newsMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsMessageExists(id))
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



        // POST: api/NewsMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsMessage>> PostNewsMessage(NewsMessage newsMessage)
        {
            _context.NewsMessage.Add(newsMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsMessage", new { id = newsMessage.id }, newsMessage);
        }

        // DELETE: api/NewsMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsMessage(int? id)
        {
            var newsMessage = await _context.NewsMessage.FindAsync(id);
            if (newsMessage == null)
            {
                return NotFound();
            }

            _context.NewsMessage.Remove(newsMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsMessageExists(int? id)
        {
            return _context.NewsMessage.Any(e => e.id == id);
        }
    }
}
