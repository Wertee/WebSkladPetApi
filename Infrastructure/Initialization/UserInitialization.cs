using Application.Interfaces;
using Domain.Entity;

namespace Infrastructure.Initialization
{
    public class UserInitialization
    {
        public static async Task Initialize(IUserService userService)
        {
            var user = new User() { Username = "Admin", Password = "Asdf1234" };

            user.Password = userService.HashPassword(user.Password);

            await userService.CreateAsync(user);
        }
    }
}
