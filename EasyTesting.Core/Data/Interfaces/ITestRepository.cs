using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Data
{
    public interface ITestRepository
    {
        Task AddTestAsync(Test test);
        Task<(IEnumerable<Test>, int Total)> GetAllTestAsync(QueryParameters parameters, int teacherId);
        Task<(IEnumerable<Test>, int Total)> GetTestsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId);
        Task<Test?> FindTestByIdAsync(int teacherId, int id);
        Task<Test?> FindTestByIdAsync(int id);
        Task DeleteTestAsync(int teacherId, int id);
    }
}
