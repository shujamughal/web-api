using Lecture2_API_Security.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lecture2_API_Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username == "user" && model.Password == "password")
            {
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, model.Username), // 'Sub' represents the subject of the token, typically the user's unique identifier like username.
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // 'Jti' (JWT ID) is a unique identifier for the token to prevent replay attacks.

            // Additional useful claims in real-world scenarios could include:
            // - 'Email' to store the user's email address.
            // - 'Role' to represent user roles for role-based authorization.
            // - Custom claims for application-specific information, such as 'Department' or 'AccessLevel'.
        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-my-secret-key-that-is-too-long-key-it-is-needed"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7228/",
                    audience: "https://localhost:7228/",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }
    }
}
