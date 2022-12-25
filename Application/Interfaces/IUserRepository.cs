using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.User.DTO;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(Domain.Entity.User user);
        Domain.Entity.User Get(UserDto userDto);
    }
}
