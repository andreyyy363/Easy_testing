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
    /// Data Transfer Object for returning question details.
    /// </summary>
    public class QuestionDTO
    {
        /// <summary>
        /// The unique identifier of the question.
        /// </summary>
        [Required(ErrorMessage = "Question ID is required.")]
        public int Id { get; set; }

        /// <summary>
        /// The text content of the question.
        /// </summary>
        [Required(ErrorMessage = "Question text is required.")]
        [MaxLength(300, ErrorMessage = "Question text cannot exceed 300 characters.")]
        public required string Text { get; set; }

        /// <summary>
        /// The name of the subject the question belongs to.
        /// </summary>
        [Required(ErrorMessage = "Subject name is required.")]
        public required string Subject { get; set; }

        [Required(ErrorMessage = "Subject Id is required.")]
        public required int SubjectId { get; set; }

        /// <summary>
        /// ID of the Teacher, who created the question.
        /// </summary>
        [Required(ErrorMessage = "Teacher ID is required.")]
        public int TeacherId { get; set; }

        /// <summary>
        /// The list of answer options associated with the question.
        /// </summary>
        [Required(ErrorMessage = "Answer options must be provided.")]
        [MinLength(2, ErrorMessage = "At least two answer options are required.")]
        public required List<AnswerOptionDTO> AnswerOptions { get; set; }

        /// <summary>
        /// Maps a Question entity to a QuestionDto.
        /// </summary>
        /// <param name="question">The Question entity.</param>
        /// <returns>The corresponding QuestionDto.</returns>
        public static QuestionDTO toDTO(Question question)
        {
            return new QuestionDTO
            {
                Id = question.Id,
                Text = question.Text,
                Subject = question.Subject?.Name ?? string.Empty,
                SubjectId = question.SubjectId,
                TeacherId = question.CreatedById,
                AnswerOptions = question.AnswerOptions.Select(AnswerOptionDTO.toDTO).ToList()
            };
        }
    }
}
