﻿using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
using EasyTesting.Core.Service;
using EasyTesting.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Web.Controllers
{
    /// <summary>
    /// API controller for managing subjects.
    /// </summary>
    [ApiController]
    [Route("api/v1/subjects")]
    [Authorize]
    public class SubjectController : BaseApiController
    {
        private readonly ISubjectService _subjectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectController"/> class.
        /// </summary>
        /// <param name="subjectService">Service for handling subject operations.</param>
        /// <param name="tokenService">Service for handling token operations.</param>
        public SubjectController(ISubjectService subjectService, TokenService tokenService)
        : base(tokenService)
        {
            _subjectService = subjectService;

        }

        /// <summary>
        /// Retrieves all subjects associated with the currently authenticated teacher.
        /// </summary>
        /// <param name="parameters">Paging parameters.</param>
        /// <returns>
        /// 200 OK with a list of subject DTOs, or 401 Unauthorized if not authenticated.
        /// </returns>
        /// <response code="200">Returns a list of subjects.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects([FromQuery] QueryParameters parameters)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var subjects = await _subjectService.GetAllSubjectsAsync(parameters, teacherId.Value);
            var result = new ResultBuilder<SubjectDTO>()
                .WithPagedResult(subjects)
                .WithStatusCode(StatusCodes.Status200OK)
                .WithContentType("application/json")
                .Build();

            return result;
        }

        /// <summary>
        /// Retrieves a specific subject by its ID for the currently authenticated teacher.
        /// </summary>
        /// <param name="id">The ID of the subject to retrieve.</param>
        /// <returns>
        /// 200 OK with the subject DTO if found,  
        /// 401 Unauthorized if not authenticated,  
        /// or 404 Not Found if the subject does not exist.
        /// </returns>
        /// <response code="200">Returns the requested subject.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If the subject was not found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var subject = await _subjectService.FindSubjectByIdAsync(teacherId.Value, id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        /// <summary>
        /// Adds a new subject for the currently authenticated teacher.
        /// </summary>
        /// <param name="subjectDTO">The DTO containing the subject's name.</param>
        /// <returns>
        /// 204 No Content if the subject was successfully added,  
        /// or 401 Unauthorized if not authenticated.
        /// </returns>
        /// <response code="204">Subject was successfully added.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] CreateSubjectDTO subjectDTO)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            await _subjectService.AddSubjectAsync(teacherId.Value, subjectDTO);
            return NoContent();
        }

        /// <summary>
        /// Deletes a subject by its ID for the currently authenticated teacher.
        /// </summary>
        /// <param name="id">The ID of the subject to delete.</param>
        /// <returns>
        /// 204 No Content if the subject was successfully deleted,  
        /// or 401 Unauthorized if not authenticated.
        /// </returns>
        /// <response code="204">Subject was successfully deleted.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            await _subjectService.DeleteSubjectAsync(teacherId.Value, id);
            return NoContent();
        }
    }
}
