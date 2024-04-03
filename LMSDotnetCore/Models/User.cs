using Microsoft.AspNetCore.Identity;

namespace LMSDotnetCore.Models
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int RoleType { get; set; }
    }
}