using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using StudentPerformanceTrackerPredictionService.Models;

namespace StudentPerformanceTrackerPredictionService.Services
{
    public class PredictionService
    {
        private readonly HttpClient _httpClient;

        public PredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Prediction> GetPredictionAsync(string subject, DateTime futureDate)
        {
            var studySessions = await GetStudySessionsAsync();
            var totalHours = TotalWeightedHours(subject, studySessions);
            var projectedHours = ProjectFutureHours(subject, futureDate, studySessions);
            var totalProjectedHours = totalHours + projectedHours;

            string predictedGrade;
            if (totalProjectedHours > 90)
            {
                predictedGrade = "A";
            }
            else if (totalProjectedHours > 80)
            {
                predictedGrade = "B";
            }
            else if (totalProjectedHours > 70)
            {
                predictedGrade = "C";
            }
            else if (totalProjectedHours > 60)
            {
                predictedGrade = "D";
            }
            else
            {
                predictedGrade = "F";
            }

            return new Prediction
            {
                Subject = subject,
                FutureDate = futureDate,
                PredictedGrade = predictedGrade,
                GradeProgress = (int)(totalProjectedHours / 100 * 100),
                TotalHours = (int)totalHours
            };
        }

        private async Task<List<StudySession>> GetStudySessionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StudySession>>("https://studysessionmicroservice-gchqd8ghbnb6brfx.southeastasia-01.azurewebsites.net/api/studysessions");
        }

        private double ProjectFutureHours(string subject, DateTime futureDate, List<StudySession> studySessions)
        {
            var sessions = studySessions.Where(s => s.Subject == subject);
            if (!sessions.Any())
            {
                return 0;
            }

            var totalDays = (DateTime.Now - sessions.Min(s => s.Date)).TotalDays;
            var totalHours = sessions.Sum(s => s.Duration);
            var dailyAverageHours = totalHours / totalDays;

            var futureDays = (futureDate - DateTime.Now).TotalDays;

            return dailyAverageHours * futureDays;
        }

        public double TotalWeightedHours(string subject, List<StudySession> studySessions)
        {
            var sessions = studySessions.Where(s => s.Subject == subject);
            double weightedHours = 0;

            foreach (var session in sessions)
            {
                double weight = session.KnowledgeLevel switch
                {
                    "Beginner" => 1.0,
                    "Intermediate" => 1.5,
                    "Advanced" => 2.0,
                    _ => 1.0
                };
                weightedHours += session.Duration * weight;
            }

            return weightedHours;
        }
    }
}
