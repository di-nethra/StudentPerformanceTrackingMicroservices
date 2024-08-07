namespace StudentPerformanceTracker.Models
{
    public class StudySession
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public int Duration { get; set; }
        public string KnowledgeLevel { get; set; }
    }
}
