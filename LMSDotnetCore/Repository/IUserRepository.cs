using LMSDotnetCore.Models;

namespace LMSDotnetCore.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserNameAsync(string userName);
        Task CreateUserAsync(User user, string password);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
