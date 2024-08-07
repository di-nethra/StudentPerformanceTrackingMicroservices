using StudentPerformanceTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerformanceTracker.Services
{
    public class StudySessionService
    {
        private readonly List<StudySession> _studySessions = new List<StudySession>();

        public List<StudySession> GetAll()
        {
            return _studySessions;
        }

        public StudySession GetById(string id)
        {
            return _studySessions.FirstOrDefault(s => s.Id == id);
        }

        public void Add(StudySession studySession)
        {
            studySession.Id = Guid.NewGuid().ToString();
            _studySessions.Add(studySession);
        }

        public void Update(StudySession studySession)
        {
            var existing = GetById(studySession.Id);
            if (existing != null)
            {
                existing.Date = studySession.Date;
                existing.Subject = studySession.Subject;
                existing.Duration = studySession.Duration;
                existing.KnowledgeLevel = studySession.KnowledgeLevel;
            }
        }

        public void Delete(string id)
        {
            var studySession = GetById(id);
            if (studySession != null)
            {
                _studySessions.Remove(studySession);
            }
        }
    }
}
