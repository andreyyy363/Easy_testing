using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Data
{
    public interface ISubjectRepository
    {
        Task<(IEnumerable<Subject>, int Total)> GetAllSubjectsAsync(QueryParameters parameters, int teacherId);
        Task<Subject?> FindSubjectByIdAsync(int teacherId, int id);
        Task AddSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int teacherId, int id);
    }
}
