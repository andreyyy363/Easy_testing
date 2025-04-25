using EasyTesting.Core.Data;
using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.TestXmlModels;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace EasyTesting.Core.Service
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestGenerator _testGenerator;
        private readonly ILogger<TestService> _logger;
        public TestService(
            ITestRepository testRepository, 
            IQuestionRepository questionRepository,
            ITestGenerator testGenerator,
            ILogger<TestService> logger)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _testGenerator = testGenerator;
            _logger = logger;
        }

        public async Task CreateTestAsync(int teacherId, CreateTestDTO createTestDTO)
        {
            var questions = await _questionRepository.GetQuestionsBySubjectIdAsync(teacherId, createTestDTO.SubjectId);
            var randomQuestions = questions.OrderBy(_ => Guid.NewGuid())
                .Take(createTestDTO.QuestionsCount)
                .Where(q => q.TestId == null)
                .ToList();

            if (randomQuestions.Count < createTestDTO.QuestionsCount) 
            {
                _logger.LogError($"Not enough questions that will be added to test for selected subject: count {randomQuestions.Count}");
                throw new ArgumentException($"Not enough questions for selected subject: Count {randomQuestions.Count}");
            }

            var test = new Test() 
            { 
                Name = createTestDTO.Name,
                SubjectId = createTestDTO.SubjectId,
                Questions = randomQuestions,
                TeacherId = teacherId
            };
            
            test.TestXml = _testGenerator.GenerateTest(test);
            await _testRepository.AddTestAsync(test);
        }

        public async Task<TestResultDTO> SubmitTestAsync(SubmitTestDTO submitTestDTO)
        {
            var test = await _testRepository.FindTestByIdAsync(submitTestDTO.TestId);
            if (test == null)
            {
                _logger.LogError($"Test with id: {submitTestDTO.TestId} was not found.");
                throw new ArgumentException($"Test with id: {submitTestDTO.TestId} was not found.");
            }

            int correctAnswers = 0;
            foreach (var pair in submitTestDTO.Answers)
            {
                var question = test.Questions.FirstOrDefault(q => q.Id == pair.Key);
                if(question == null)
                {
                    _logger.LogWarning($"Question with id: {pair.Key} was not found.");
                    continue;
                }

                var answer = question.AnswerOptions.FirstOrDefault(a => a.Id == pair.Value);
                if(answer == null)
                {
                    _logger.LogWarning($"Answer Option with id: {pair.Value} was not found.");
                    continue;
                }

                if(answer.IsCorrect)
                { 
                    correctAnswers++;
                }
            }

            return new TestResultDTO
            {
                Score = correctAnswers,
                Total = test.Questions.Count
            };
        }

        public async Task<IEnumerable<Test>> GetAllTestAsync(int teacherId)
        {
            return await _testRepository.GetAllTestAsync(teacherId);
        }

        public async Task<IEnumerable<Test>> GetTestsBySubjectIdAsync(int teacherId, int subjectId)
        {
            return await _testRepository.GetTestsBySubjectIdAsync(teacherId, subjectId);
        }

        public async Task<Test?> FindTestByIdAsync(int teacherId, int id)
        {
            return await _testRepository.FindTestByIdAsync(teacherId, id);
        }

        public async Task DeleteTestAsync(int teacherId, int id)
        {
            await _testRepository.DeleteTestAsync(teacherId, id);
        }
    }
}
