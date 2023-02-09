using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;
using Application.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Application.User.DTO;

namespace WebSkladPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        private static readonly User user = new User() { Username = "Admin", Password = "1234" };

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            try
            {
                var userToken = _service.AuthenticateUser(userDto);
                if (!string.IsNullOrEmpty(userToken))
                {
                    return Ok(new { access_token = userToken });
                }
            }
            catch (UserLoginException exception)
            {
                return Unauthorized(exception.Message);
            }

            return Unauthorized();
        }


    }
}
