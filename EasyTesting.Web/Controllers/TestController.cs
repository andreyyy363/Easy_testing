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
    /// API controller for managing tests.
    /// </summary>
    [ApiController]
    [Route("api/v1/tests")]
    [Authorize]
    public class TestController : BaseApiController
    {
        private readonly ITestService _testService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="testService">Service for handling test logic.</param>
        /// <param name="tokenService">Service for handling token operations.</param>
        public TestController(ITestService testService, TokenService tokenService)
        : base(tokenService)
        {
            _testService = testService;
        }

        /// <summary>
        /// Retrieves all tests available to the current teacher.
        /// </summary>
        /// <param name="parameters">Paging parameters.</param>
        /// <returns>List of all test DTOs associated with the authenticated teacher.</returns>
        /// <response code="200">Returns the list of tests.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllTests([FromQuery] QueryParameters parameters)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var tests = await _testService.GetAllTestAsync(parameters, teacherId.Value);
            var result = new ResultBuilder<TestDTO>()
                .WithPagedResult(tests)
                .WithStatusCode(StatusCodes.Status200OK)
                .WithContentType("application/json")
                .Build();

            return result;
        }

        /// <summary>
        /// Retrieves a test by its ID.
        /// </summary>
        /// <param name="id">The ID of the test.</param>
        /// <returns>The test DTO if found.</returns>
        /// <response code="200">Returns the test.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If the test is not found.</response>
        [HttpGet("{id}")]
        [Obsolete("Use {id}/xml instead")]
        public async Task<IActionResult> GetTest(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var test = await _testService.FindTestByIdAsync(teacherId.Value, id);
            if (test == null)
                return NotFound();

            return Ok(test);
        }

        /// <summary>
        /// Retrieves a test by its ID.
        /// </summary>
        /// <param name="id">The ID of the test.</param>
        /// <returns>The test DTO if found.</returns>
        /// <response code="200">Returns the test.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If the test is not found.</response>
        [HttpGet("{id}/xml")]
        public async Task<IActionResult> GetTestXml(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)

                return Unauthorized();
            var test = await _testService.FindTestByIdAsync(teacherId.Value, id);
            if (test == null)
                return NotFound();

            return Content(test.TestXml, "application/xml");
        }

        /// <summary>
        /// Retrieves all tests for a specified subject.
        /// </summary>
        /// <param name="id">The ID of the subject.</param>
        /// <param name="parameters">Paging parameters.</param>
        /// <returns>List of tests for the subject.</returns>
        /// <response code="200">Returns the list of tests for the subject.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpGet("subject/{id}")]
        public async Task<IActionResult> GetTestsBySubject(int id, [FromQuery] QueryParameters parameters)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            var tests = await _testService.GetTestsBySubjectIdAsync(parameters, teacherId.Value, id);
            var result = new ResultBuilder<TestDTO>()
                .WithPagedResult(tests)
                .WithStatusCode(StatusCodes.Status200OK)
                .WithContentType("application/json")
                .Build();

            return result;
        }

        /// <summary>
        /// Generates a new test with randomly selected questions for a subject.
        /// </summary>
        /// <param name="createTestDTO">DTO containing subject ID, test name, and number of questions.</param>
        /// <returns>NoContent if successfully created.</returns>
        /// <response code="204">If the test is created successfully.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [Authorize(Roles = "Teacher")]
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateTest([FromBody] CreateTestDTO createTestDTO)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            await _testService.CreateTestAsync(teacherId.Value, createTestDTO);
            return NoContent();
        }

        /// <summary>
        /// Submits a student's test answers and returns their score.
        /// </summary>
        /// <param name="submitTestDTO">The submitted test data, including TestId, StudentId, and a dictionary of answers.</param>
        /// <returns>The test result with score and total questions.</returns>
        /// <response code="200">If the submission was successful.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="400">If the test or question data is invalid.</response>
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitTest([FromBody] SubmitTestDTO submitTestDTO)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var result = await _testService.SubmitTestAsync(submitTestDTO);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a test by its ID.
        /// </summary>
        /// <param name="id">The ID of the test to delete.</param>
        /// <returns>NoContent on successful deletion.</returns>
        /// <response code="204">If the test was successfully deleted.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var teacherId = GetTeacherId();
            if (teacherId == null)
                return Unauthorized();

            await _testService.DeleteTestAsync(teacherId.Value, id);
            return NoContent();
        }
    }
}
