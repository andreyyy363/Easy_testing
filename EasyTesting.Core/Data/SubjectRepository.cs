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

        public async Task<Subject?> FindSubjectByIdAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
