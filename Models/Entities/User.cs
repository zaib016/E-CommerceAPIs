namespace E_CommerceAPIs.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        //public string Role { get; set; }
    }
}
