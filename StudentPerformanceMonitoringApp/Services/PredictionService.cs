using System;
using System.Linq;
using StudentPerformanceTracker.Models;
using StudentPerformanceTracker.Services;

namespace StudentPerformanceTracker.Services
{
    public class PredictionService
    {
        private readonly StudySessionService _studySessionService;

        public PredictionService(StudySessionService studySessionService)
        {
            _studySessionService = studySessionService;
        }

        public string PredictedGrade(string subject, DateTime futureDate)
        {
            var totalHours = TotalWeightedHours(subject);
            var projectedHours = ProjectFutureHours(subject, futureDate);
            var totalProjectedHours = totalHours + projectedHours;

            if (totalProjectedHours > 90)
            {
                return "A";
            }
            if (totalProjectedHours > 80)
            {
                return "B";
            }
            if (totalProjectedHours > 70)
            {
                return "C";
            }
            if (totalProjectedHours > 60)
            {
                return "D";
            }
            return "F";
        }

        private double ProjectFutureHours(string subject, DateTime futureDate)
        {
            var sessions = _studySessionService.GetAll().Where(s => s.Subject == subject);
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

        public double TotalWeightedHours(string subject)
        {
            var sessions = _studySessionService.GetAll().Where(s => s.Subject == subject);
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

        public int GradeProgress(string subject, DateTime futureDate)
        {
            var totalHours = TotalWeightedHours(subject);
            var projectedHours = ProjectFutureHours(subject, futureDate);
            var totalProjectedHours = totalHours + projectedHours;
            return (int)(totalProjectedHours / 100 * 100);
        }

        public int TotalHours(string subject)
        {
            return _studySessionService.GetAll().Where(s => s.Subject == subject).Sum(s => s.Duration);
        }
    }
}
