using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyTesting.Web.Controllers
{
    /// <summary>
    /// Controller for managing users, including retrieving, updating, and deleting user data.
    /// Accessible only by authorized users.
    /// </summary>
    [ApiController]
    [Route("api/v1/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenService _tokenGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">Service for user-related operations.</param>
        /// <param name="tokenGenerator">Service for handling token operations.</param>
        public UserController(IUserService userService, TokenService tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>
        /// 200 OK with a list of all user DTOs.
        /// </returns>
        /// <response code="200">Returns the list of users.</response>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            var usersDto = users.Select(UserDTO.toDTO).ToList();
            return Ok(usersDto);
        }

        /// <summary>
        /// Retrieves the currently authenticated user's information.
        /// </summary>
        /// <returns>
        /// 200 OK with user details, 401 if unauthorized, or 404 if user not found.
        /// </returns>
        /// <response code="200">Returns the current user's details.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If the user does not exist.</response>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _tokenGenerator.GetTeacherIdFromToken(Request);
            if (userId == null)
                return Unauthorized();

            var user = await _userService.FindUserByIdAsync(userId.Value);
            if (user == null)
                return NotFound();

            return Ok(UserDTO.toDTO(user));
        }

        /// <summary>
        /// Updates the currently authenticated user's information.
        /// </summary>
        /// <param name="userUpdateDTO">The DTO containing updated user details.</param>
        /// <returns>
        /// 200 OK with updated user data, 401 if unauthorized, or 404 if user not found.
        /// </returns>
        /// <response code="200">Returns the updated user details.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="404">If the user does not exist.</response>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UserUpdateDTO userUpdateDTO)
        {
            var userId = _tokenGenerator.GetTeacherIdFromToken(Request);
            if (userId == null)
                return Unauthorized();

            var user = await _userService.FindUserByIdAsync(userId.Value);
            if (user == null)
                return NotFound();

            var updatedUser = await _userService.UpdateUserAsync(userUpdateDTO, user);
            return Ok(updatedUser);
        }

        /// <summary>
        /// Deletes the currently authenticated user.
        /// </summary>
        /// <returns>
        /// 204 No Content if successful, 401 if unauthorized.
        /// </returns>
        /// <response code="204">User was successfully deleted.</response>
        /// <response code="401">If the user is not authenticated.</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = _tokenGenerator.GetTeacherIdFromToken(Request);
            if (userId == null)
                return Unauthorized();

            await _userService.DeleteUserAsync(userId.Value);
            return NoContent();
        }
    }
}
