using Application.User.DTO;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        string AuthenticateUser(UserDto userDto);
    }
}
