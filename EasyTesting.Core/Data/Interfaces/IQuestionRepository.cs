using EasyTesting.Core.Models.Entity;

namespace EasyTesting.Core.Data
{
    public interface IQuestionRepository
    {
        Task AddQuestionAsync(Question question);
        Task<Question?> FindQuestionByIdAsync(int teacherId, int id);
        Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int teacherId, int subjectId);
        Task<IEnumerable<Question>> GetAllQuestionsAsync(int teacherId);
        Task<Question> UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int teacherId, int id);
    }
}
