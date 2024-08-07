using Microsoft.AspNetCore.Mvc;
using StudentPerformanceTrackerPredictionService.Models;
using StudentPerformanceTrackerPredictionService.Services;
using System;
using System.Threading.Tasks;

namespace StudentPerformanceTrackerPredictionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionsController : ControllerBase
    {
        private readonly PredictionService _predictionService;

        public PredictionsController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet]
        public async Task<ActionResult<Prediction>> GetPrediction([FromQuery] string subject, [FromQuery] DateTime futureDate)
        {
            var prediction = await _predictionService.GetPredictionAsync(subject, futureDate);
            return Ok(prediction);
        }
    }
}
