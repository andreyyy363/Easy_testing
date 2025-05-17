using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
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

        public async Task<(IEnumerable<Test>, int Total)> GetAllTestAsync(QueryParameters parameters, int teacherId)
        {
            var query = _context.Tests.Where(t => t.TeacherId == teacherId);
            var total = await query.CountAsync();
            return (await query.Include(t => t.Questions).ThenInclude(q => q.AnswerOptions)
                               .Include(t => t.Subject).ToListAsync(), total);
        }

        public async Task<(IEnumerable<Test>, int Total)> GetTestsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId)
        {
            var query = _context.Tests.Where(t => t.SubjectId == subjectId && t.TeacherId == teacherId);
            var total = await query.CountAsync();
            return (await query.Include(t => t.Subject).AsSplitQuery().ToListAsync(), total);
        }

    }
}
