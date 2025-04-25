using EasyTesting.Core.Data;
using EasyTesting.Core.Models.Entity;

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

        public Task<Subject?> FindSubjectByIdAsync(int teacherId, int id)
        {
            return _subjectRepository.FindSubjectByIdAsync(teacherId, id);
        }

        public Task<IEnumerable<Subject>> GetAllSubjectsAsync(int teacherId)
        {
            return _subjectRepository.GetAllSubjectsAsync(teacherId);
        }

        public async Task DeleteSubjectAsync(int teacherId, int id)
        {
            await _subjectRepository.DeleteSubjectAsync(teacherId, id);
        }
    }
}
