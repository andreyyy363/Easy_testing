using EasyTesting.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EasyTesting.Core.Data
{
    public class TestRepository : ITestRepository
    {
        private readonly AppDbContext _context;
        public TestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTestAsync(int teacherId, int id)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(t => t.Id == id && t.TeacherId == teacherId);

            if(test != null)
            {
                _context.Tests.Remove(test); 
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Test?> FindTestByIdAsync(int teacherId, int id)
        {
            return await _context.Tests
                .Include(t => t.Questions).ThenInclude(q => q.AnswerOptions)
                .Include(t => t.Subject)
                .AsSplitQuery()
                .FirstOrDefaultAsync(t => t.Id == id && t.TeacherId == teacherId);
        }

        public async Task<Test?> FindTestByIdAsync(int id)
        {
            return await _context.Tests
                .Include(t => t.Questions).ThenInclude(q => q.AnswerOptions)
                .Include(t => t.Subject)
                .AsSplitQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Test>> GetAllTestAsync(int teacherId)
        {
            return await _context.Tests
                .Include(t => t.Questions).ThenInclude(q => q.AnswerOptions)
                .Include(t => t.Subject)
                .Where(t => t.TeacherId == teacherId)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Test>> GetTestsBySubjectIdAsync(int teacherId, int subjectId)
        {
            return await _context.Tests
                .Include(t => t.Subject)
                .Where(t => t.SubjectId == subjectId && t.TeacherId == teacherId)
                .AsSplitQuery()
                .ToListAsync();
        }
    }
}
