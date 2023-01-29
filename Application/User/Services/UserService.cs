using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.User.DTO;

namespace Application.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task CreateAsync(Domain.Entity.User user)
        {
            await _userRepository.CreateAsync(user);
        }

        public Domain.Entity.User Get(UserDto userDto)
        {
            userDto.Password = HashPassword(userDto.Password);
            var existingUser = _userRepository.Get(userDto);
            return existingUser;
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
