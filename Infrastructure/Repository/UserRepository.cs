using Application.Interfaces;
using Application.User.DTO;
using Domain.Entity;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly WebSkladUsersContext _context;
        public UserRepository(WebSkladUsersContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public User Get(UserDto userDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == userDto.Username && x.Password == userDto.Password);
            if (user == null)
                return null;
            return user;
        }
    }
}
