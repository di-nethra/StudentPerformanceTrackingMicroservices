using StudentPerformanceTrackerStudySessionService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerformanceTrackerStudySessionService.Services
{
    public class StudySessionService
    {
        private readonly MongoDBService<StudySession> _mongoDbService;

        public StudySessionService(MongoDBService<StudySession> mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public Task<List<StudySession>> GetAllAsync() => _mongoDbService.GetAllAsync();

        public Task<StudySession> GetByIdAsync(string id) => _mongoDbService.GetByIdAsync(id);

        public Task CreateAsync(StudySession studySession) => _mongoDbService.CreateAsync(studySession);

        public Task UpdateAsync(string id, StudySession studySession) => _mongoDbService.UpdateAsync(id, studySession);

        public Task DeleteAsync(string id) => _mongoDbService.DeleteAsync(id);
    }
}
