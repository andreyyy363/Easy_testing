using EasyTesting.Core.Models.Entity;

namespace EasyTesting.Core.Service
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync(int teacherId);
        Task<Subject?> FindSubjectByIdAsync(int teacherId, int id);
        Task AddSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int teacherId, int id);
    }
}
