using EasyTesting.Core.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a user.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// This property is required.
        /// </summary>
        [Required]
        public required long Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// This property is required and must be between 3 and 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// This property is required and must be a valid email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be valid")]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// This property is required.
        /// </summary>
        [Required(ErrorMessage = "User should have a role")]
        public required string Role { get; set; }

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
        /// Converts a <see cref="User"/> entity to a <see cref="UserDTO"/>.
        /// </summary>
        /// <param name="user">The user entity to convert to a DTO.</param>
        /// <returns>A <see cref="UserDTO"/> containing the user data.</returns>
        public static UserDTO toDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Role = user.Role.ToString()
            };
        }
    }

}
