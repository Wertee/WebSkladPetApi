using Application.User.DTO;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(Domain.Entity.User user);
        Domain.Entity.User Get(UserDto userDto);

        string HashPassword(string password);

    }
}
