using EasyTesting.Core.Data;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
using EasyTesting.Core.Service;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace EasyTesting.Tests.Service
{
    [TestFixture]
    public class QuestionServiceTests
    {
        private IQuestionRepository _questionRepository;
        private ILogger<QuestionService> _logger;
        private IQuestionService _questionService;

        [SetUp]
        public void SetUp()
        {
            _questionRepository = Substitute.For<IQuestionRepository>();
            _logger = Substitute.For<ILogger<QuestionService>>();
            _questionService = new QuestionService(_questionRepository, _logger);
        }

        [Test]
        public async Task GetAllQuestionsAsync_ShouldReturnPagedResult_WhenCalled()
        {
            // Arrange
            var teacherId = 1;
            var parameters = new QueryParameters { skip = 0, limit = 10 };
            var questions = new List<Question>
            {
                new Question { Id = 1, Text = "What is 2+2?", SubjectId = 1 },
                new Question { Id = 2, Text = "What is 3+3?", SubjectId = 1 }
            };
            _questionRepository.GetAllQuestionsAsync(parameters, teacherId)
                .Returns((questions, questions.Count));

            // Act
            var result = await _questionService.GetAllQuestionsAsync(parameters, teacherId);

            // Assert
            Assert.That(result.Data.Count(), Is.EqualTo(2));
            Assert.That(result.Total, Is.EqualTo(2));
            Assert.That(result.Data.First().Text, Is.EqualTo("What is 2+2?"));
        }

        [Test]
        public async Task FindQuestionByIdAsync_ShouldReturnQuestion_WhenIdIsValid()
        {
            // Arrange
            var teacherId = 1;
            var questionId = 1;
            var question = new Question { Id = questionId, Text = "What is 2+2?", SubjectId = 1 };
            _questionRepository.FindQuestionByIdAsync(teacherId, questionId).Returns(question);

            // Act
            var result = await _questionService.FindQuestionByIdAsync(teacherId, questionId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(questionId));
            Assert.That(result.Text, Is.EqualTo("What is 2+2?"));
        }

        [Test]
        public async Task GetQuestionsBySubjectIdAsync_ShouldReturnPagedResult_WhenSubjectIdIsValid()
        {
            // Arrange
            var teacherId = 1;
            var subjectId = 2;
            var parameters = new QueryParameters { skip = 0, limit = 10 };
            var questions = new List<Question>
            {
                new Question { Id = 1, Text = "What is 2+2?", SubjectId = subjectId },
                new Question { Id = 2, Text = "What is 3+3?", SubjectId = subjectId }
            };
            _questionRepository.GetQuestionsBySubjectIdAsync(parameters, teacherId, subjectId)
                .Returns((questions, questions.Count));

            // Act
            var result = await _questionService.GetQuestionsBySubjectIdAsync(parameters, teacherId, subjectId);

            // Assert
            Assert.That(result.Data.Count(), Is.EqualTo(2));
            Assert.That(result.Total, Is.EqualTo(2));
            Assert.That(result.Data.First().Text, Is.EqualTo("What is 2+2?"));
        }
    }
}