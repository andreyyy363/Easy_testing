using EasyTesting.Core.Data;
using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EasyTesting.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenService _tokenGenerator;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            TokenService tokenGenerator,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _logger = logger;          
        }

        public async Task<User> Register(UserCreateDTO userCreateDTO)
        {
            var user = await _userRepository.FindUserByEmail(userCreateDTO.Email);
            if (user != null)
            {
                _logger.LogError($"Email:{userCreateDTO.Email} is already used");
                throw new ArgumentException("Email is already used");
            }
            var passwordHash = _passwordHasher.HashPassword(userCreateDTO.Password);
            user = userCreateDTO.fromDtoWithHashedPassword(passwordHash);
            await _userRepository.AddUserAsync(user);
            
            return user;
        }

        public async Task Login(HttpContext httpContext, string username, string password)
        {
            var user = await _userRepository.FindUserByUsername(username);
            if (user == null)
            {
                _logger.LogError($"User with username: {username} not found");
                throw new ArgumentException($"User with username: {username} not found");
            }

            var correctPassword = _passwordHasher.VerifyPassword(user.PasswordHash, password);
            if (!correctPassword)
            {
                _logger.LogError($"Password is incorect");
                throw new UnauthorizedAccessException("Password is incorect");
            }
            var token = _tokenGenerator.GenerateJwtToken(user);

            httpContext.Response.Cookies.Append(TokenService.AuthTokenKey, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        public Task Logout(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete("AuthToken");
            return Task.CompletedTask;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> FindUserByIdAsync(int id)
        {
            return await _userRepository.FindUserByIdAsync(id);
        }

        public async Task<User> UpdateUserAsync(UserUpdateDTO userUpdateDTO, User user)
        {        
            UpdateUser(userUpdateDTO, user);
            await _userRepository.UpdateUserAsync(user);
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        private void UpdateUser(UserUpdateDTO userUpdateDTO, User user)
        {
            if (userUpdateDTO?.Password != null)
            {
                user.PasswordHash = _passwordHasher.HashPassword(userUpdateDTO.Password);
            }

            user.Username = userUpdateDTO?.Username == null ? user.Username : userUpdateDTO.Username;
            user.FirstName = userUpdateDTO?.FirstName == null ? user.FirstName : userUpdateDTO.FirstName;
            user.LastName = userUpdateDTO?.LastName == null ? user.LastName : userUpdateDTO.LastName;
            user.Email = userUpdateDTO?.Email == null ? user.Email : userUpdateDTO.Email;
            user.UpdatedAt = DateTime.Now;
        }
    }
}
