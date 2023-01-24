using KeyMaster.Context;
using KeyMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KeyMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresModelsController : BaseController
    {
        private readonly ApplicationDBContext _context;

        public ScoresModelsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/ScoresModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoreModel>>> GetScoresModel()
        {
            return await _context.Scores.ToListAsync();
        }

        // GET: api/ScoresModels/5
        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<ScoreModel>>> GetUsersScoresModel()
        {
            // var scoresModel = await _context.Scores.FindAsync(id);
            
            var scoresModel = await _context.Scores.Where(x => x.UserId == GetUserId()).ToListAsync();
           
            if (scoresModel == null)
            {
                return NotFound();
            }

            return scoresModel;
        }

        // PUT: api/ScoresModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScoresModel(int id, ScoreModel scoresModel)
        {
            if (id != scoresModel.ScoresID)
            {
                return BadRequest();
            }

            _context.Entry(scoresModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoresModelExists(id))
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

        // POST: api/ScoresModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ScoreModel>> PostScoresModel(ScoreModel scoresModel)
        {
            scoresModel.UserId= GetUserId();
            _context.Scores.Add(scoresModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScoresModel", new { id = scoresModel.ScoresID }, scoresModel);
        }

        // DELETE: api/ScoresModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScoresModel(int id)
        {
            var scoresModel = await _context.Scores.FindAsync(id);
            if (scoresModel == null)
            {
                return NotFound();
            }

            _context.Scores.Remove(scoresModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScoresModelExists(int id)
        {
            return _context.Scores.Any(e => e.ScoresID == id);
        }
    }
}
