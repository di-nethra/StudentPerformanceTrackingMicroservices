using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StudentPerformanceTrackerStudySessionService.Models
{
    public class StudySession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public int Duration { get; set; }
        public string KnowledgeLevel { get; set; }
    }
}
