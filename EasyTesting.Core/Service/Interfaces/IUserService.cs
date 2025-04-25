using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using Microsoft.AspNetCore.Http;

namespace EasyTesting.Core.Service
{
    public interface IUserService
    {
        Task<User> Register(UserCreateDTO userCreateDTO);
        Task Login(HttpContext httpContext, string username, string password);
        Task Logout(HttpContext httpContext);
        Task<User?> FindUserByIdAsync(int userId);
        Task<List<User>> GetAllAsync();
        Task<User> UpdateUserAsync(UserUpdateDTO userUpdateDTO, User user);
        Task DeleteUserAsync(int userId);
    }
}
