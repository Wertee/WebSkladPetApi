using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Exceptions;
using Application.Interfaces;
using Application.User.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication.Services
{
    public class UserAuthenticationService : IAuthenticationService
    {
        private readonly IOptions<AuthenticationOption> _authOptions;
        private readonly IUserService _userService;

        public UserAuthenticationService(IOptions<AuthenticationOption> authOptions, IUserService userService)
        {
            _authOptions = authOptions;
            _userService = userService;
        }

        public string AuthenticateUser(UserDto userDto)
        {
            var usr = _userService.Get(userDto);
            if (usr == null)
                throw new UserLoginException("Wrong username or password");
            return GenerateJWT(usr);

        }

        private string GenerateJWT(Domain.Entity.User user)
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
