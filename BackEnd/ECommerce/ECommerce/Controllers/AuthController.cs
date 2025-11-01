using Microsoft.AspNetCore.Mvc;
using ECommerce.DTOs;
using ECommerce.Services;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly ITokenService _tokens;

        public AuthController(IUserRepository users, ITokenService tokens)
        {
            _users = users;
            _tokens = tokens;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _users.GetByUsername(req.Username);
            if (user == null) return Unauthorized("Invalid credentials");
            if (user.Password != req.Password) return Unauthorized("Invalid credentials");

            var token = _tokens.CreateToken(user);

            return Ok(new LoginResponse { Token = token });
        }
    }
}