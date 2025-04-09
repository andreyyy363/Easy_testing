using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Data Transfer Object used to update an existing question.
    /// </summary>
    public class UpdateQuestionDto
    {
        /// <summary>
        /// Updated text of the question.
        /// </summary>
        [MaxLength(300, ErrorMessage = "Question text cannot exceed 300 characters.")]
        public string? Text { get; set; }

        /// <summary>
        /// Updated list of answer options for the question.
        /// </summary>
        public List<AnswerOptionDto>? AnswerOptions { get; set; }
    }
}
