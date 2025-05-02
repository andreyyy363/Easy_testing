using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using Microsoft.AspNetCore.Http;

namespace EasyTesting.Core.Service
{
    public interface IUserService
    {
        Task<User> Register(UserCreateDTO userCreateDTO);
        Task<string> Login(HttpContext httpContext, string username, string password);
        Task<User?> FindUserByIdAsync(int userId);
        Task<List<User>> GetAllAsync();
        Task<User> UpdateUserAsync(UserUpdateDTO userUpdateDTO, User user);
        Task DeleteUserAsync(int userId);
    }
}
