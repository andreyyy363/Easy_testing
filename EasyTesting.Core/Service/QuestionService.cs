using EasyTesting.Core.Data;
using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EasyTesting.Core.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(IQuestionRepository questionRepository, ILogger<QuestionService> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync(int teacherId)
        {
            return await _questionRepository.GetAllQuestionsAsync(teacherId);
        }

        public async Task<Question?> FindQuestionByIdAsync(int teacherId, int id)
        {
            return await _questionRepository.FindQuestionByIdAsync(teacherId, id);
        }

        public async Task<IEnumerable<Question>> GetQuestionsBySubjectIdAsync(int teacherId, int subjectId)
        {
            return await _questionRepository.GetQuestionsBySubjectIdAsync(teacherId, subjectId);
        }

        public async Task AddQuestionAsync(int teacherId, CreateQuestionDTO createQuestionDto)
        {
            createQuestionDto.TeacherId = teacherId;
            await _questionRepository.AddQuestionAsync(createQuestionDto.fromDTO());
        }

        public async Task<Question> UpdateQuestionAsync(int teacherId, int id, UpdateQuestionDTO updateQuestionDto)
        {
            var question = await _questionRepository.FindQuestionByIdAsync(teacherId, id);
            if (question == null)
            {
                _logger.LogError($"Question {id} was not found.");
                throw new ArgumentException($"Question {id} was not found.");
            }

            question.Text = updateQuestionDto.Text.IsNullOrEmpty() ? question.Text : question.Text;
            question.AnswerOptions = updateQuestionDto.AnswerOptions == null ? 
                question.AnswerOptions : 
                updateQuestionDto.AnswerOptions.Select(a => new AnswerOption
                {
                    Text = a.OptionText,
                    IsCorrect = a.IsCorrect
                }).ToList();

            return await _questionRepository.UpdateQuestionAsync(question);
        }

        public async Task DeleteQuestionAsync(int teacherId, int id)
        {
            await _questionRepository.DeleteQuestionAsync(teacherId, id);
        }
    }
}
