namespace E_CommerceAPIs.Models
{
    public class UserDTOs
    {
        public class RegisterDTOs
        {
            public required string Username { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
        public class LoginDTOs
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
    }
}
