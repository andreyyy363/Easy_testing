using EasyTesting.Core.Models.Entity;

namespace EasyTesting.Core.Data
{
    public interface IQuestionRepository
    {
        Task AddQuestionAsync(Question question);
        Task<Question?> FindQuestionByIdAsync(int id);
        Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int subjectId);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int id);
    }
}
