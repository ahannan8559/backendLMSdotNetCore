using LMSDotnetCore.Authentication;
using LMSDotnetCore.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;

namespace LMSDotnetCore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null && await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}
