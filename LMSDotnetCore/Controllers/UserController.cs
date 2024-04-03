using LMSDotnetCore.Data_Transfer_Objects;
using LMSDotnetCore.Models;
using LMSDotnetCore.Repository;
using LMSDotnetCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMSDotnetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var isValid = await _authService.ValidateCredentialsAsync(model.UserName, model.Password);
            if (!isValid)
                return BadRequest("Invalid username or password");

            return Ok();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(RegisterDto model)
        {
            var existingUser = await _authService.GetUserByUserNameAsync(model.UserName);
            if (existingUser != null)
                return BadRequest("Username is already in use");

            var newUser = new User
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleType = model.RoleType
            };

            var result = await _authService.RegisterUserAsync(newUser, model.Password);
            if (!result.Succeeded)
                return BadRequest("Failed to register user");

            return Ok();
        }
    }

}
