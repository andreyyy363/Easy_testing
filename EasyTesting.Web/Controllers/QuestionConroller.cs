using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Web.Controllers
{
    /// <summary>
    /// Controller for managing questions.
    /// </summary>
    [ApiController]
    [Route("api/v1/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionController"/> class.
        /// </summary>
        /// <param name="questionService">Service for handling question operations.</param>
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Retrieves all questions.
        /// </summary>
        /// <returns>A list of questions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            return Ok(questions.Select(QuestionDto.toDTO).ToList());
        }

        /// <summary>
        /// Creates a new question.
        /// </summary>
        /// <param name="createQuestionDto">The question data.</param>
        /// <returns>NoContent if successful.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            await _questionService.AddQuestionAsync(createQuestionDto);
            return NoContent();
        }

        /// <summary>
        /// Retrieves a specific question by its ID.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>The requested question, or NotFound if it doesn't exist.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _questionService.FindQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(QuestionDto.toDTO(question));
        }

        /// <summary>
        /// Retrieves all questions belonging to a specific subject.
        /// </summary>
        /// <param name="id">The ID of the subject.</param>
        /// <returns>A list of questions for the specified subject.</returns>
        [HttpGet("subject/{id}")]
        public async Task<IActionResult> GetQuestionsBySubject(int id)
        {
            var questions = await _questionService.GetQuestionsBySubjectIdAsync(id);
            return Ok(questions.Select(QuestionDto.toDTO).ToList());
        }

        /// <summary>
        /// Updates an existing question.
        /// </summary>
        /// <param name="id">The ID of the question to update.</param>
        /// <param name="updateQuestionDto">The updated question data.</param>
        /// <returns>The updated question, or NotFound if it doesn't exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] UpdateQuestionDto updateQuestionDto)
        {
            var question = await _questionService.UpdateQuestionAsync(id, updateQuestionDto);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(QuestionDto.toDTO(question));
        }

        /// <summary>
        /// Deletes a question by its ID.
        /// </summary>
        /// <param name="id">The ID of the question.</param>
        /// <returns>NoContent if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }
    }

}
