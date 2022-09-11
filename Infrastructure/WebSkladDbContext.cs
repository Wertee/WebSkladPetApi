﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class WebSkladDbContext : DbContext
    {
        public WebSkladDbContext(DbContextOptions<WebSkladDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
