using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EasyTesting.Web.Controllers
{
    /// <summary>
    /// API controller for managing questions.
    /// Accessible only by authenticated teachers.
    /// </summary>
    [ApiController]
    [Route("api/v1/questions")]
    [Authorize(Roles = "Teacher")]
    public class QuestionController : BaseApiController
    {
        private readonly IQuestionService _questionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionController"/> class.
        /// </summary>
        /// <param name="questionService">Service for handling question operations.</param>
        /// <param name="tokenService">Service for handling token operations.</param>
        public QuestionController(IQuestionService questionService, TokenService tokenService)
        : base(tokenService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Retrieves all questions for the currently authenticated teacher.
        /// </summary>
        /// <returns>
        /// 200 OK with a list of questions, or 401 Unauthorized if not authenticated.
        /// </returns>
        /// <response code="200">Returns a list of questions.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var questions = await _questionService.GetAllQuestionsAsync(teacherId.Value);
            return Ok(questions.Select(QuestionDTO.toDTO).ToList());
        }

        /// <summary>
        /// Creates a new question for the currently authenticated teacher.
        /// </summary>
        /// <param name="createQuestionDto">The question data.</param>
        /// <returns>
        /// 204 No Content if the question was successfully created,  
        /// or 401 Unauthorized if not authenticated.
        /// </returns>
        /// <response code="204">Question was successfully created.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDTO createQuestionDto)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            await _questionService.AddQuestionAsync(teacherId.Value, createQuestionDto);
            return NoContent();
        }

        /// <summary>
        /// Retrieves a specific question by its ID.
        /// </summary>
        /// <param name="id">The ID of the question to retrieve.</param>
        /// <returns>
        /// 200 OK with the question DTO if found,  
        /// 401 Unauthorized if not authenticated,  
        /// or 404 Not Found if the question does not exist.
        /// </returns>
        /// <response code="200">Returns the requested question.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If the question was not found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var question = await _questionService.FindQuestionByIdAsync(teacherId.Value, id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(QuestionDTO.toDTO(question));
        }

        /// <summary>
        /// Retrieves all questions belonging to a specific subject.
        /// </summary>
        /// <param name="id">The ID of the subject.</param>
        /// <returns>
        /// 200 OK with a list of questions for the specified subject,  
        /// or 401 Unauthorized if not authenticated.
        /// </returns>
        /// <response code="200">Returns a list of questions for the subject.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpGet("subject/{id}")]
        public async Task<IActionResult> GetQuestionsBySubject(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var questions = await _questionService.GetQuestionsBySubjectIdAsync(teacherId.Value, id);
            return Ok(questions.Select(QuestionDTO.toDTO).ToList());
        }

        /// <summary>
        /// Updates an existing question.
        /// </summary>
        /// <param name="id">The ID of the question to update.</param>
        /// <param name="updateQuestionDto">The updated question data.</param>
        /// <returns>
        /// 200 OK with the updated question if successful,  
        /// 401 Unauthorized if not authenticated,  
        /// or 404 Not Found if the question does not exist.
        /// </returns>
        /// <response code="200">Question was successfully updated.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If the question was not found.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] UpdateQuestionDTO updateQuestionDto)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var question = await _questionService.UpdateQuestionAsync(teacherId.Value, id, updateQuestionDto);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(QuestionDTO.toDTO(question));
        }

        /// <summary>
        /// Deletes a question by its ID.
        /// </summary>
        /// <param name="id">The ID of the question to delete.</param>
        /// <returns>
        /// 204 No Content if the question was successfully deleted,  
        /// or 401 Unauthorized if not authenticated.
        /// </returns>
        /// <response code="204">Question was successfully deleted.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            await _questionService.DeleteQuestionAsync(teacherId.Value, id);
            return NoContent();
        }
    }
}
