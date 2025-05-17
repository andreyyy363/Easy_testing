using EasyTesting.Core.Models.DTO;
using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;
using Microsoft.AspNetCore.Http;

namespace EasyTesting.Core.Service
{
    public interface IUserService
    {
        Task<UserDTO> Register(UserCreateDTO userCreateDTO);
        Task<(string token, string expires)> Login(HttpContext httpContext, string username, string password);
        Task<User?> FindUserByIdAsync(int userId);
        Task<PagedResult<UserDTO>> GetAllAsync(QueryParameters parameters);
        Task<UserDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO, User user);
        Task DeleteUserAsync(int userId);
    }
}
