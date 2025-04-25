using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for updating user information.
    /// </summary>
    public class UserUpdateDTO
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// The username must be between 3 and 50 characters.
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// The password must be between 8 and 100 characters.
        /// </summary>
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// The email must be in a valid format.
        /// </summary>
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// The first name can be up to 50 characters in length.
        /// </summary>
        [MaxLength(50)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// The last name can be up to 50 characters in length.
        /// </summary>
        [MaxLength(50)]
        public string? LastName { get; set; }
    }
}
