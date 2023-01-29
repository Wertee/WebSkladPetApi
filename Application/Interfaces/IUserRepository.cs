using Application.User.DTO;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(Domain.Entity.User user);
        Domain.Entity.User Get(UserDto userDto);
    }
}
