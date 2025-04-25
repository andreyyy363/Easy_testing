using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Data Transfer Object used to create a new test.
    /// </summary>
    public class CreateTestDTO
    {
        /// <summary>
        /// Name of the test to be created.
        /// </summary>
        [Required(ErrorMessage = "Test name is required.")]
        public required string Name { get; set; }

        /// <summary>
        /// ID of the subject associated with the test.
        /// </summary>
        [Required(ErrorMessage = "Subject ID is required.")]
        public int SubjectId { get; set; }

        /// <summary>
        /// ID of the Teacher, who created the test.
        /// </summary>
        [Required(ErrorMessage = "Teacher ID is required.")]
        public int TeacherId { get; set; }

        /// <summary>
        /// Number of questions that should be included in the test.
        /// </summary>
        [Required(ErrorMessage = "Number of questions is required.")]
        public int QuestionsCount { get; set; }
    }
}
