using Microsoft.AspNetCore.Mvc;
using StudentPerformanceTrackerStudySessionService.Models;
using StudentPerformanceTrackerStudySessionService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerformanceTrackerStudySessionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudySessionsController : ControllerBase
    {
        private readonly StudySessionService _studySessionService;

        public StudySessionsController(StudySessionService studySessionService)
        {
            _studySessionService = studySessionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudySession>>> GetAll()
        {
            var studySessions = await _studySessionService.GetAllAsync();
            return Ok(studySessions);
        }

        [HttpGet("{id:length(24)}", Name = "GetStudySession")]
        public async Task<ActionResult<StudySession>> Get(string id)
        {
            var studySession = await _studySessionService.GetByIdAsync(id);

            if (studySession == null)
            {
                return NotFound();
            }

            return Ok(studySession);
        }

        [HttpPost]
        public async Task<ActionResult<StudySession>> Create(StudySession studySession)
        {
            await _studySessionService.CreateAsync(studySession);

            return CreatedAtRoute("GetStudySession", new { id = studySession.Id.ToString() }, studySession);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, StudySession studySession)
        {
            var existingStudySession = await _studySessionService.GetByIdAsync(id);

            if (existingStudySession == null)
            {
                return NotFound();
            }

            await _studySessionService.UpdateAsync(id, studySession);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var studySession = await _studySessionService.GetByIdAsync(id);

            if (studySession == null)
            {
                return NotFound();
            }

            await _studySessionService.DeleteAsync(id);

            return NoContent();
        }
    }
}
