using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class WebSkladUsersContext : DbContext
    {
        public WebSkladUsersContext(DbContextOptions<WebSkladUsersContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
