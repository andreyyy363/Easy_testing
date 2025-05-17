using EasyTesting.Core.Models.Entity;
using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Data
{
    public interface IUserRepository
    {
        Task<User?> FindUserByIdAsync(int userId);
        Task<User?> FindUserByUsername(string username);
        Task<User?> FindUserByEmail(string email);
        Task<(IEnumerable<User>, int Total)> GetAllUsersAsync(QueryParameters parameters);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }

}
