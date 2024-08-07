using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StudentPerformanceTrackerBreakService.Models
{
    public class Break
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime Date { get; set; }
        public string BreakType { get; set; }
        public int Duration { get; set; }
    }
}
