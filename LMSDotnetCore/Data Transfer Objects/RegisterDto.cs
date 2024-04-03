namespace LMSDotnetCore.Data_Transfer_Objects
{
    public class RegisterDto
    {
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int RoleType { get; set; }
    }
}
