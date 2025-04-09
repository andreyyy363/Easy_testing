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
    /// Data Transfer Object for creating a new question.
    /// </summary>
    public class CreateQuestionDto
    {
        /// <summary>
        /// The text of the question.
        /// </summary>
        [Required(ErrorMessage = "Question text is required.")]
        [MaxLength(500, ErrorMessage = "Question text cannot exceed 500 characters.")]
        public required string Text { get; set; }

        /// <summary>
        /// The ID of the subject to which the question belongs.
        /// </summary>
        [Required(ErrorMessage = "Subject ID is required.")]
        public int SubjectId { get; set; }

        /// <summary>
        /// The list of possible answer options for the question.
        /// </summary>
        [Required(ErrorMessage = "At least one answer option is required.")]
        [MinLength(2, ErrorMessage = "A question must have at least two answer options.")]
        public required List<AnswerOptionDto> AnswerOptions { get; set; }

        /// <summary>
        /// Converts the DTO to a Question entity.
        /// </summary>
        /// <returns>The corresponding Question entity.</returns>
        public Question fromDTO()
        {
            return new Question
            {
                Text = this.Text,
                SubjectId = this.SubjectId,
                AnswerOptions = AnswerOptions.Select(a => new AnswerOption
                {
                    Text = a.OptionText,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
        }
    }
}
