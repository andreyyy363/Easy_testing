using EasyTesting.Core.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for creating a user.
    /// </summary>
    public class UserCreateDTO
    {
        /// <summary>
        /// Gets or sets the username for the user.
        /// This property is required and must be between 3 and 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for the user.
        /// This property is required and must be between 8 and 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the email for the user.
        /// This property is required and should be a valid email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// This property is optional and can be up to 50 characters long.
        /// </summary>
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// This property is optional and can be up to 50 characters long.
        /// </summary>
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Converts the DTO to a User entity, including the hashed password.
        /// </summary>
        /// <param name="passwordHash">The hashed password to be assigned to the user.</param>
        /// <returns>A <see cref="User"/> entity with the information from the DTO.</returns>
        public User fromDtoWithHashedPassword(string passwordHash)
        {
            return new User
            {
                Username = this.Username,
                Email = this.Email,
                PasswordHash = passwordHash,
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }
    }

}
