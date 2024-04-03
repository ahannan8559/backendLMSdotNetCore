using LMSDotnetCore.Models;
using LMSDotnetCore.Repository;
using Microsoft.AspNetCore.Identity;

namespace LMSDotnetCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public AuthService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        
        public async Task<bool> ValidateCredentialsAsync(string userName, string password)
        {
            var user = await _userRepository.GetUserByUserNameAsync(userName);
            if (user == null)
                return false;

            return await _userRepository.CheckPasswordAsync(user, password);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetUserByUserNameAsync(userName);
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }

}
