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
    /// Data Transfer Object for representing an answer option in a question.
    /// </summary>
    public class AnswerOptionDTO
    {
        /// <summary>
        /// Unique identifier of the answer option.
        /// </summary>
        [Required(ErrorMessage = "Option ID is required.")]
        public int Id { get; set; }

        /// <summary>
        /// The text of the answer option.
        /// </summary>
        [Required(ErrorMessage = "Option Text is required.")]
        [MaxLength(100, ErrorMessage = "Option Text cannot exceed 100 characters.")]
        public required string OptionText { get; set; }

        /// <summary>
        /// Indicates whether this answer option is correct.
        /// </summary>
        [Required(ErrorMessage = "IsCorrect must be specified.")]
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Converts an AnswerOption entity to an AnswerOptionDto.
        /// </summary>
        /// <param name="answerOption">The AnswerOption entity to convert.</param>
        /// <returns>The corresponding AnswerOptionDto.</returns>
        public static AnswerOptionDTO toDTO(AnswerOption answerOption)
        {
            return new AnswerOptionDTO
            {
                OptionText = answerOption.Text,
                IsCorrect = answerOption.IsCorrect,
                Id = answerOption.Id
            };
        }
    }
}
