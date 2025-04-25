using EasyTesting.Core.Models.Entity;

namespace EasyTesting.Core.Data
{
    public interface IUserRepository
    {
        Task<User?> FindUserByIdAsync(int userId);
        Task<User?> FindUserByUsername(string username);
        Task<User?> FindUserByEmail(string email);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }

}
