using LMSDotnetCore.Models;
using Microsoft.AspNetCore.Identity;

namespace LMSDotnetCore.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateCredentialsAsync(string userName, string password);
        Task<User?> GetUserByUserNameAsync(string userName);
        Task<IdentityResult> RegisterUserAsync(User user, string password);
    }
}
