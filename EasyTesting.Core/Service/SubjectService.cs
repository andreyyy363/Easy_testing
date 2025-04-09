using EasyTesting.Core.Data;
using EasyTesting.Core.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            await _subjectRepository.AddSubjectAsync(subject);
        }

        public Task<Subject?> FindSubjectByIdAsync(int id)
        {
            return _subjectRepository.FindSubjectByIdAsync(id);
        }

        public Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return _subjectRepository.GetAllSubjectsAsync();
        }

        public async Task DeleteSubjectAsync(int id)
        {
            await _subjectRepository.DeleteSubjectAsync(id);
        }
    }
}
