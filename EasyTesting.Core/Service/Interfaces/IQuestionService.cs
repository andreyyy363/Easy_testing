using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;

namespace EasyTesting.Core.Service
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync(int teacherId);
        Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int teacherId, int subjectId);
        Task<Question?> FindQuestionByIdAsync(int teacherId, int id);
        Task AddQuestionAsync(int teacherId, CreateQuestionDTO createQuestionDto);
        Task<Question> UpdateQuestionAsync(int teacherId, int id, UpdateQuestionDTO updateQuestionDto);
        Task DeleteQuestionAsync(int teacherId, int id);

    }
}
