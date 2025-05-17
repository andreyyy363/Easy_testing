using EasyTesting.Core.Data;
using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
using Microsoft.Extensions.Logging;

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

            if (createTestDTO.QuestionsCount == 0 || randomQuestions.Count < createTestDTO.QuestionsCount) 
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

        public async Task<PagedResult<TestDTO>> GetAllTestAsync(QueryParameters parameters, int teacherId)
        {
            (var data, var total) = await _testRepository.GetAllTestAsync(parameters, teacherId);
            var tests = data.Select(TestDTO.toDTO);
            return PagedResult<TestDTO>.Create(tests, total, parameters.skip, parameters.limit);
        }

        public async Task<PagedResult<TestDTO>> GetTestsBySubjectIdAsync(QueryParameters parameters, int teacherId, int subjectId)
        {
            (var data, var total) = await _testRepository.GetTestsBySubjectIdAsync(parameters, teacherId, subjectId);
            var tests = data.Select(TestDTO.toDTO);
            return PagedResult<TestDTO>.Create(tests, total, parameters.skip, parameters.limit);
        }

        public async Task<TestDTO?> FindTestByIdAsync(int teacherId, int id)
        {
           var test = await _testRepository.FindTestByIdAsync(teacherId, id);
           if(test == null)
               return null;

           return TestDTO.toDTO(test);
        }

        public async Task DeleteTestAsync(int teacherId, int id)
        {
            await _testRepository.DeleteTestAsync(teacherId, id);
        }
    }
}
