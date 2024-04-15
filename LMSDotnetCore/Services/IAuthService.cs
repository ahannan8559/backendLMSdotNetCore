using LMSDotnetCore.Models;

namespace LMSDotnetCore.Services
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(User user, string password);
        string GetJwtToken(User user);
        Task<User?> IsValidUser(string username, string password);
        void checkJwtValid(string id);
    }
}
