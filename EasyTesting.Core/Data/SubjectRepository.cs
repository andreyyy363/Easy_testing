using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
using EasyTesting.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace EasyTesting.Core.Data
{
    public class SubjectRepository: ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task<Subject?> FindSubjectByIdAsync(int teacherId, int id)
        {
            return await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id && s.TeacherId == teacherId);
        }

        public async Task<(IEnumerable<Subject>, int Total)> GetAllSubjectsAsync(QueryParameters parameters, int teacherId)
        {
            var query = _context.Subjects.Where(s => s.TeacherId == teacherId);
            var total = await query.CountAsync();
            return (await query.ApplyPaging(parameters).ToListAsync(), total);
        }

        public async Task DeleteSubjectAsync(int teacherId, int id)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id && s.TeacherId == teacherId);
            
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
