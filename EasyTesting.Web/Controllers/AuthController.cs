using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Web.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and management.
    /// Provides endpoints for registering, logging in, and logging out users.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userService">Service for user-related operations.</param>
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userCreateDTO">DTO containing the user's registration data.</param>
        /// <returns>
        /// 200 OK with the registered user data,  
        /// or 400 Bad Request if registration fails.
        /// </returns>
        /// <response code="200">User was successfully registered.</response>
        /// <response code="400">If the registration data is invalid or registration failed.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserCreateDTO userCreateDTO)
        {
            var user = await _userService.Register(userCreateDTO);
            return Ok(UserDTO.toDTO(user));
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userLoginDTO">DTO containing user login credentials.</param>
        /// <returns>
        /// 200 OK with a success message if login is successful,  
        /// 400 Bad Request if credentials are invalid.
        /// </returns>
        /// <response code="200">Login was successful.</response>
        /// <response code="400">If the provided credentials are invalid.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDTO userLoginDTO)
        {
            await _userService.Login(HttpContext, userLoginDTO.Username, userLoginDTO.Password);
            return Ok(new { Message = "Login successful" });
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>
        /// 204 No Content if logout is successful.
        /// </returns>
        /// <response code="204">Logout was successful.</response>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout(HttpContext);
            return NoContent();
        }
    }
}
