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
    /// Data Transfer Object representing a subject.
    /// </summary>
    public class SubjectDTO
    {
        /// <summary>
        /// Unique identifier of the subject.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the subject.
        /// </summary>
        [Required(ErrorMessage = "Subject name is required.")]
        [MaxLength(100, ErrorMessage = "Subject name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the teacher who owns the subject.
        /// </summary>
        [Required(ErrorMessage = "Teacher ID is required.")]
        public int TeacherId { get; set; } 

        /// <summary>
        /// Maps a Subject entity to a SubjectDto.
        /// </summary>
        /// <param name="subject">The Subject entity.</param>
        /// <returns>The corresponding SubjectDto.</returns>
        public static SubjectDTO toDTO(Subject subject)
        {
            return new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
                TeacherId = subject.TeacherId
            };
        }
    }
}
