using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Service
{
    public interface ITestService
    {
        Task CreateTestAsync(int teacherId, CreateTestDTO createTestDTO);
        Task<TestResultDTO> SubmitTestAsync(SubmitTestDTO submitTestDTO);
        Task<PagedResult<TestDTO>> GetAllTestAsync(QueryParameters parameters, int teacherId);
        Task<PagedResult<TestDTO>> GetTestsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId);
        Task<TestDTO?> FindTestByIdAsync(int teacherId, int id);
        Task DeleteTestAsync(int teacherId, int id);
    }
}
