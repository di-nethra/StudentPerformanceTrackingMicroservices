namespace StudentPerformanceTracker.Models
{
    public class Prediction
    {
        public string Subject { get; set; }
        public DateTime FutureDate { get; set; }
        public string PredictedGrade { get; set; }
        public int GradeProgress { get; set; }
        public int TotalHours { get; set; }
    }
}