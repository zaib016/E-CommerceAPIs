using E_CommerceAPIs.Models;
using E_CommerceAPIs.Models.Entities;

namespace E_CommerceAPIs.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User?> GetByUserIdAsync(int id);
        Task<User> AddUserAsync(User user);
        Task SaveChangesAsync();
    }
    public interface IUserService
    {
        Task<string> RegisterAsync(UserDTOs.RegisterDTOs registerDTOs);
        Task<string> LoginAsync(UserDTOs.LoginDTOs loginDTOs);
        Task<User?> GetByIdAsync(int id);
    }
    public interface ITokenService
    {
        string CreateToken(User user);
    }
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
