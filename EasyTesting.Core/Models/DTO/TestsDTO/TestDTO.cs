using EasyTesting.Core.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Data Transfer Object representing a test.
    /// </summary>
    public class TestDTO
    {
        /// <summary>
        /// Unique identifier of the test.
        /// </summary>
        [Required(ErrorMessage = "Test ID is required.")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the test.
        /// </summary>
        [Required(ErrorMessage = "Test name is required.")]
        public required string Name { get; set; }

        /// <summary>
        /// ID of the subject related to the test.
        /// </summary>
        [Required(ErrorMessage = "Subject ID is required.")]
        public int SubjectId { get; set; }

        /// <summary>
        /// ID of the Teacher, who created the test.
        /// </summary>
        [Required(ErrorMessage = "Teacher ID is required.")]
        public int TeacherId { get; set; }

        /// <summary>
        /// Number of questions included in the test.
        /// </summary>
        [Required(ErrorMessage = "Questions count is required.")]
        public int QuestionsCount { get; set; }

        /// <summary>
        /// Name of the subject related to the test.
        /// </summary>
        [Required(ErrorMessage = "Subject name is required.")]
        public required string Subject { get; set; }

        /// <summary>
        /// List of questions included in the test.
        /// </summary>
        //public List<QuestionDTO> Questions { get; set; } = new();

        /// <summary>
        /// List of questions included in the test.
        /// </summary>
        public required string TestXml { get; set; }

        /// <summary>
        /// Maps a Test entity to its corresponding DTO.
        /// </summary>
        /// <param name="test">The test entity.</param>
        /// <returns>A populated TestDTO.</returns>
        public static TestDTO toDTO(Test test)
        {
            return new TestDTO
            {
                Id = test.Id,
                Name = test.Name,
                QuestionsCount = test.Questions.Count,
                Subject = test.Subject?.Name ?? string.Empty,
                SubjectId = test.SubjectId,
                TeacherId = test.TeacherId,
                TestXml = test.TestXml
            };
        }
    }
}
