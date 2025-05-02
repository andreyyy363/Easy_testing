using EasyTesting.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync(int teacherId)
        {
            return await _context.Subjects.Where(s => s.TeacherId == teacherId).ToListAsync();
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
