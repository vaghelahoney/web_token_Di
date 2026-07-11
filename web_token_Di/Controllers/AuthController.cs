using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_token_Di.Models.DTOs;
using web_token_Di.Repositories;
using Microsoft.AspNetCore.RateLimiting;

namespace web_token_Di.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.RegisterAsync(model);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User registered successfully!" });
        }

        [EnableRateLimiting("ApiPolicy")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //Prakash@example.com Admin@124
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authRepository.LoginAsync(model);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Invalid email/username or password." });
            }

            return Ok(new { Token = token, Message = "Login successful!" });
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok(new { Message = "Successfully accessed Admin-only endpoint." });
        }

        [HttpGet("user-or-admin")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UserOrAdminEndpoint()
        {
            return Ok(new { Message = "Successfully accessed User or Admin endpoint." });
        }

        [HttpGet("user-only")]
        [Authorize(Roles = "User")]
        public IActionResult UserEndpoint()
        {
            return Ok(new { Message = "Successfully accessed User or Admin endpoint." });
        }
    }
}
