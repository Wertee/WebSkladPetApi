using Application.Authentication;
using Application.Authentication.DTO;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebSkladPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IOptions<AuthenticationOption> _authOptions;

        private static readonly User user = new User() { Username = "Admin", Password = "1234" };

        public AuthenticationController(IOptions<AuthenticationOption> authOptions)
        {
            _authOptions = authOptions;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO userDto)
        {
            return Ok(user);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO userDto)
        {

            var user = AuthenticateUser(userDto.Username, userDto.Password);
            if (user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new { access_token = token }
            );
            }

            return Unauthorized();
        }

        private User AuthenticateUser(string username, string password)
        {
            if (user.Username == username && user.Password == password)
                return user;
            return null;
        }

        private string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name,user.Username),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
