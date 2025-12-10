using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using E_CommerceAPIs.Data;
using E_CommerceAPIs.Models;
using E_CommerceAPIs.Models.Entities;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_CommerceAPIs.Repository
{
    public class UserRepository : DbProvider, IUserRepository, ITokenService, IEmailService
    {
        private IConfiguration _config;

        public UserRepository(ApplicationDbContext dbContext, IConfiguration config) : base(dbContext)
        {
            _config = config;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<User?> GetByUserIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email.ToString()),
                new Claim("username", user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                       issuer: _config["Jwt:Issuer"],
                       audience: _config["Jwt:Audience"],
                       claims: claims,
                       expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinute"])),
                       signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var fromEmail = _config["EmailSetting:FromEmail"];
            var password = _config["EmailSetting:Password"];

            var mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(fromEmail, password);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;     // MUST be false
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtp.SendMailAsync(mail);
            }
        }
        public class UserService : DbProvider, IUserService
        {
            private IUserRepository _userRepo;
            private ITokenService _tokenRepo;

            public UserService(ApplicationDbContext dbContext, IUserRepository userRepository, ITokenService tokenService) : base(dbContext)
            {
                _userRepo = userRepository;
                _tokenRepo = tokenService;
            }

            public async Task<User?> GetByIdAsync(int id)
            {
                return await _dbContext.Users.FirstOrDefaultAsync(i => i.UserId == id);
            }

            public async Task<string> LoginAsync(UserDTOs.LoginDTOs loginDTOs)
            {
                var user = await _userRepo.GetByEmailAsync(loginDTOs.Email);
                if (user == null) return "Invalid email or password";

                var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTOs.Password, user.PasswordHash);
                if (!isPasswordValid) return "Invalid email or password";

                return _tokenRepo.CreateToken(user);
            }

            public async Task<string> RegisterAsync(UserDTOs.RegisterDTOs registerDTOs)
            {
                var exists = await _userRepo.GetByEmailAsync(registerDTOs.Email);
                if (exists != null) return "Email already exists";

                var user = new User
                {
                    Username = registerDTOs.Username,
                    Email = registerDTOs.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTOs.Password)
                };

                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return "User Register Successfully";

            }
        }
    }
}
