using LMSDotnetCore.Models;

namespace LMSDotnetCore.Repository
{

    public interface IUserRepository
    {
        Task<bool> CheckUsernameExistsAsync(string username);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> IsValidUserAsync(string username, string password);
        Task RegisterUserAsync(User user, string password);
        Task<User?> GetUserByUsernameAsync(string username);
    }

}
