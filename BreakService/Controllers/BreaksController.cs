using Microsoft.AspNetCore.Mvc;
using StudentPerformanceTrackerBreakService.Models;
using StudentPerformanceTrackerBreakService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerformanceTrackerBreakService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreaksController : ControllerBase
    {
        private readonly BreakService _breakService;

        public BreaksController(BreakService breakService)
        {
            _breakService = breakService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Break>>> GetAll()
        {
            var breaks = await _breakService.GetAllAsync();
            return Ok(breaks);
        }

        [HttpGet("{id:length(24)}", Name = "GetBreak")]
        public async Task<ActionResult<Break>> Get(string id)
        {
            // Log the ID
            Console.WriteLine($"Getting break with ID: {id}");

            var studentBreak = await _breakService.GetByIdAsync(id);

            if (studentBreak == null)
            {
                return NotFound();
            }

            return Ok(studentBreak);
        }

        [HttpPost]
        public async Task<ActionResult<Break>> Create(Break studentBreak)
        {
            await _breakService.CreateAsync(studentBreak);

            return CreatedAtRoute("GetBreak", new { id = studentBreak.Id.ToString() }, studentBreak);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Break studentBreak)
        {
            var existingBreak = await _breakService.GetByIdAsync(id);

            if (existingBreak == null)
            {
                return NotFound();
            }

            await _breakService.UpdateAsync(id, studentBreak);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var studentBreak = await _breakService.GetByIdAsync(id);

            if (studentBreak == null)
            {
                return NotFound();
            }

            await _breakService.DeleteAsync(id);

            return NoContent();
        }
    }
}
