using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Data
{
    public interface IQuestionRepository
    {
        Task AddQuestionAsync(Question question);
        Task<Question?> FindQuestionByIdAsync(int teacherId, int id);
        Task<(IEnumerable<Question>, int Total)> GetQuestionsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId);
        Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int teacherId, int subjectId);
        Task<(IEnumerable<Question>, int Total)> GetAllQuestionsAsync(QueryParameters parameters, int teacherId);
        Task<Question> UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int teacherId, int id);
    }
}
