using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Web.Controllers
{
    /// <summary>
    /// Controller for managing subjects.
    /// </summary>
    [ApiController]
    [Route("api/v1/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectController"/> class.
        /// </summary>
        /// <param name="subjectService">Service for handling subject operations.</param>
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        /// <summary>
        /// Retrieves all subjects.
        /// </summary>
        /// <returns>A list of subjects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects.Select(SubjectDto.toDTO).ToList());
        }

        /// <summary>
        /// Retrieves a specific subject by its ID.
        /// </summary>
        /// <param name="id">The ID of the subject.</param>
        /// <returns>The requested subject, or NotFound if it doesn't exist.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _subjectService.FindSubjectByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(SubjectDto.toDTO(subject));
        }

        /// <summary>
        /// Adds a new subject.
        /// </summary>
        /// <param name="subjectDto">The subject data.</param>
        /// <returns>NoContent if successful.</returns>
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] CreateSubjectDTO subjectDto)
        {
            var subject = new Subject { Name = subjectDto.Name, TeacherId = subjectDto.TeacherId };
            await _subjectService.AddSubjectAsync(subject);
            return NoContent();
        }

        /// <summary>
        /// Deletes a subject by its ID.
        /// </summary>
        /// <param name="id">The ID of the subject.</param>
        /// <returns>NoContent if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return NoContent();
        }
    }
}
