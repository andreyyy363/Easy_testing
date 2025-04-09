using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;

namespace EasyTesting.Core.Service
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int subjectId);
        Task<Question?> FindQuestionByIdAsync(int id);
        Task AddQuestionAsync(CreateQuestionDto createQuestionDto);
        Task<Question> UpdateQuestionAsync(int id, UpdateQuestionDto updateQuestionDto);
        Task DeleteQuestionAsync(int id);

    }
}
