using EasyTesting.Core.Data;
using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task AddSubjectAsync(int teacherId, CreateSubjectDTO subjectDTO)
        {
            var subject = new Subject { Name = subjectDTO.Name, TeacherId = teacherId };
            await _subjectRepository.AddSubjectAsync(subject);
        }

        public async Task<SubjectDTO?> FindSubjectByIdAsync(int teacherId, int id)
        {
            var subject = await _subjectRepository.FindSubjectByIdAsync(teacherId, id);
            if (subject == null)
                return null;

            return SubjectDTO.toDTO(subject);
        }

        public async Task<PagedResult<SubjectDTO>> GetAllSubjectsAsync(QueryParameters parameters, int teacherId)
        {
            (var data, var total) = await _subjectRepository.GetAllSubjectsAsync(parameters, teacherId);
            var subjects = data.Select(SubjectDTO.toDTO);
            return PagedResult<SubjectDTO>.Create(subjects, total, parameters.skip, parameters.limit);
        }

        public async Task DeleteSubjectAsync(int teacherId, int id)
        {
            await _subjectRepository.DeleteSubjectAsync(teacherId, id);
        }
    }
}
