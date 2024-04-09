using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LMSDotnetCore.Models;
using LMSDotnetCore.Repository;


namespace LMSDotnetCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> RegisterUserAsync(User user, string password)
        {
            if (await _userRepository.CheckUsernameExistsAsync(user.UserName!))
            {
                return "Username already exists";
            }

            if (await _userRepository.CheckEmailExistsAsync(user.Email!))
            {
                return "Email already exists";
            }

            await _userRepository.RegisterUserAsync(user, password);

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return token;
        }

        public string GetJwtToken(User user)
        {
            var token = GenerateJwtToken(user!);
            return token;
        }

        public async Task<User?> IsValidUser(string username, string password)
        {
            var isValidUser = await _userRepository.IsValidUserAsync(username, password);
            if (!isValidUser)
            {
                return null;
            }
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        private string GenerateJwtToken(User user)
        {

            var jwtKey = _configuration["JwtSettings:Key"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            if (jwtKey is null || issuer is null || audience is null)
            {
                throw new InvalidOperationException("JWT key, issuer, or audience is missing in configuration.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(issuer,
              audience,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Sectoken);
        }

        public void checkJwtValid(string jwtToken)
        {
            // Decode JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenBytes = Encoding.UTF8.GetBytes(jwtToken);
            var token = tokenHandler.ReadJwtToken(jwtToken);

            // Verify signature
            var jwtSettings = new JwtSettings(); // Load JWT settings from configuration
            var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience
            };

            try
            {
                tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
                Console.WriteLine("JWT token is valid.");
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine("JWT token validation failed: " + ex.Message);
            }

        }

        class JwtSettings
        {
            public string Key { get; } = "tkMk1fD2t34YkUMfsTvg7yrccZxfhVMG";
            public string Issuer { get; } = "https://localhost:7034/";
            public string Audience { get; } = "https://localhost:7034/";
        }
    }

}



//var jwtKey = _configuration["JwtSettings:Key"];
//if (jwtKey is null)
//{
//    throw new InvalidOperationException("JWT key is missing in configuration.");
//}

//var tokenHandler = new JwtSecurityTokenHandler();
//var key = Encoding.ASCII.GetBytes(jwtKey);
//var tokenDescriptor = new SecurityTokenDescriptor
//{
//    Subject = new ClaimsIdentity(new Claim[]
//    {
//    new Claim(ClaimTypes.Name, user.UserName!)
//    }),
//    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
//    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//};
//var token = tokenHandler.CreateToken(tokenDescriptor);
//return tokenHandler.WriteToken(token);


//    var tokenHandler = new JwtSecurityTokenHandler();
//    var key = Encoding.ASCII.GetBytes(jwtKey);
//    var tokenDescriptor = new SecurityTokenDescriptor
//    {
//        Subject = new ClaimsIdentity(new Claim[]
//        {
//new Claim(ClaimTypes.Name, user.UserName!)
//        }),
//        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
//        Audience = audience, // Set audience claim
//        Issuer = issuer,     // Set issuer claim
//        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//    };
//    var token = tokenHandler.CreateToken(tokenDescriptor);
//    return tokenHandler.WriteToken(token);