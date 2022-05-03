using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedRift.Data;
using RedRift.Models;

namespace RedRift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GameResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GameResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameResult>>> GetGameResults()
        {
            return await _context.GameResults.ToListAsync();
        }

        // GET: api/GameResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameResult>> GetGameResult(Guid id)
        {
            var gameResult = await _context.GameResults.FindAsync(id);

            if (gameResult == null)
            {
                return NotFound();
            }

            return gameResult;
        }

        // PUT: api/GameResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameResult(Guid id, GameResult gameResult)
        {
            if (id != gameResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameResultExists(id))
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

        // POST: api/GameResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameResult>> PostGameResult(GameResult gameResult)
        {
            _context.GameResults.Add(gameResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameResult", new { id = gameResult.Id }, gameResult);
        }

        // DELETE: api/GameResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameResult(Guid id)
        {
            var gameResult = await _context.GameResults.FindAsync(id);
            if (gameResult == null)
            {
                return NotFound();
            }

            _context.GameResults.Remove(gameResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameResultExists(Guid id)
        {
            return _context.GameResults.Any(e => e.Id == id);
        }
    }
}
