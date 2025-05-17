using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Service
{
    public interface IQuestionService
    {
        Task<PagedResult<QuestionDTO>> GetAllQuestionsAsync(QueryParameters parameters, int teacherId);
        Task<PagedResult<QuestionDTO>> GetQuestionsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId);
        Task<QuestionDTO?> FindQuestionByIdAsync(int teacherId, int id);
        Task AddQuestionAsync(int teacherId, CreateQuestionDTO createQuestionDto);
        Task<QuestionDTO> UpdateQuestionAsync(int teacherId, int id, UpdateQuestionDTO updateQuestionDto);
        Task DeleteQuestionAsync(int teacherId, int id);

    }
}
