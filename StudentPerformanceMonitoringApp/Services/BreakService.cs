using StudentPerformanceTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerformanceTracker.Services
{
    public class BreakService
    {
        private readonly List<Break> _breaks = new List<Break>();

        public List<Break> GetAll()
        {
            return _breaks;
        }

        public Break GetById(string id)
        {
            return _breaks.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Break studentBreak)
        {
            studentBreak.Id = Guid.NewGuid().ToString();
            _breaks.Add(studentBreak);
        }

        public void Update(Break studentBreak)
        {
            var existing = GetById(studentBreak.Id);
            if (existing != null)
            {
                existing.Date = studentBreak.Date;
                existing.BreakType = studentBreak.BreakType;
                existing.Duration = studentBreak.Duration;
            }
        }

        public void Delete(string id)
        {
            var studentBreak = GetById(id);
            if (studentBreak != null)
            {
                _breaks.Remove(studentBreak);
            }
        }
    }
}
