using E_CommerceAPIs.Models;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTOs.RegisterDTOs registerDTOs)
        {
            var result = await _userService.RegisterAsync(registerDTOs);
            if(result == "Invalid email or password")
            {
                return BadRequest();
            }

            var subject = "Welcome to our App!";
            var body = $"Hi {registerDTOs.Username},<br>Thank you for registering!</br>";
            await _emailService.SendEmailAsync(registerDTOs.Email, subject, body);

            return Ok("Registeration successful! Email sent");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTOs.LoginDTOs loginDTOs)
        {
            var token = await _userService.LoginAsync(loginDTOs);
            if (token == "Invalid email or password") return Unauthorized("Invalid email or password");

            return Ok(token);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
