using StudentPerformanceTrackerBreakService.Models;
using StudentPerformanceTrackerBreakService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerformanceTrackerBreakService.Services
{
    public class BreakService
    {
        private readonly MongoDBService<Break> _mongoDbService;

        public BreakService(MongoDBService<Break> mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public Task<List<Break>> GetAllAsync() => _mongoDbService.GetAllAsync();

        public Task<Break> GetByIdAsync(string id) => _mongoDbService.GetByIdAsync(id);

        public Task CreateAsync(Break studentBreak) => _mongoDbService.CreateAsync(studentBreak);

        public Task UpdateAsync(string id, Break studentBreak) => _mongoDbService.UpdateAsync(id, studentBreak);

        public Task DeleteAsync(string id) => _mongoDbService.DeleteAsync(id);
    }
}
