using EasyTesting.Core.Data;
using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
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

        public async Task<PagedResult<QuestionDTO>> GetAllQuestionsAsync(QueryParameters parameters, int teacherId)
        {
            (var data, var total) = await _questionRepository.GetAllQuestionsAsync(parameters, teacherId);
            var questions = data.Select(QuestionDTO.toDTO);
            return PagedResult<QuestionDTO>.Create(questions, total, parameters.skip, parameters.limit);
        }

        public async Task<QuestionDTO?> FindQuestionByIdAsync(int teacherId, int id)
        {
            var question = await _questionRepository.FindQuestionByIdAsync(teacherId, id);
            if (question == null)
                return null;

            return QuestionDTO.toDTO(question);
        }

        public async Task<PagedResult<QuestionDTO>> GetQuestionsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId)
        {
            (var data, var total) = await _questionRepository.GetQuestionsBySubjectIdAsync(parameters, teacherId, subjectId);
            var questions = data.Select(QuestionDTO.toDTO);
            return PagedResult<QuestionDTO>.Create(questions, total, parameters.skip, parameters.limit);
        }

        public async Task AddQuestionAsync(int teacherId, CreateQuestionDTO createQuestionDto)
        {
            createQuestionDto.TeacherId = teacherId;
            await _questionRepository.AddQuestionAsync(createQuestionDto.fromDTO());
        }

        public async Task<QuestionDTO> UpdateQuestionAsync(int teacherId, int id, UpdateQuestionDTO updateQuestionDto)
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

            var updatedQuestion = await _questionRepository.UpdateQuestionAsync(question);
            return QuestionDTO.toDTO(updatedQuestion);
        }

        public async Task DeleteQuestionAsync(int teacherId, int id)
        {
            await _questionRepository.DeleteQuestionAsync(teacherId, id);
        }
    }
}
