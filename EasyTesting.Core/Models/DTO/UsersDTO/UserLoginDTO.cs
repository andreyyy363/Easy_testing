using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.DTO
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for user login information.
    /// </summary>
    public class UserLoginDTO
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// This property is required and must be between 3 and 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password of the user.
        /// This property is required and must be between 8 and 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
