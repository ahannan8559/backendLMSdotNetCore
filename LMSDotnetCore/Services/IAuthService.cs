using LMSDotnetCore.Models;
using Microsoft.AspNetCore.Identity;

namespace LMSDotnetCore.Services
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(User user, string password);
        Task<string> LoginAsync(string username, string password);
        void checkJwtValid(string id);
    }
}
