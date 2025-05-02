using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Data Transfer Object for creating a new subject.
    /// </summary>
    public class CreateSubjectDTO
    {
        /// <summary>
        /// The name of the subject.
        /// </summary>
        [Required(ErrorMessage = "Subject name is required.")]
        [MaxLength(100, ErrorMessage = "Subject name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        /// <summary>
        /// The identifier of the teacher creating the subject.
        /// </summary>
        // [Required(ErrorMessage = "Teacher ID is required.")]
        public int TeacherId { get; set; }
    }
}
