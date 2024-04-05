using LMSDotnetCore.Data_Transfer_Objects;
using LMSDotnetCore.Models;
using LMSDotnetCore.Repository;
using LMSDotnetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSDotnetCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleType = model.RoleType,
            };

            var result = await _authService.RegisterUserAsync(user, model.Password);
            if (result.StartsWith("Username"))
            {
                return BadRequest(result);
            }
            else if (result.StartsWith("Email"))
            {
                return BadRequest(result);
            }

            return Ok(new { Token = result });
        }

        [HttpPost("login")]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _authService.LoginAsync(model.UserName, model.Password);
            if (result == "Invalid credentials")
            {
                return BadRequest(result);
            }
            return Ok(new { Token = result });
        }

        
        [HttpPost("logout")]
        public IActionResult Logout()
        {

            return Ok("Logout successful");
        }

        [HttpPost("check/{id}")]
        //[Authorize]
        public IActionResult check(string id)
        {
            _authService.checkJwtValid(id);
            return Ok("Checked");
        }

    }
}
