
using Application.Common.Mapping;
using Application.Product.Services;
using AutoMapper;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Tests.Common
{
    public class ContextCreator
    {

        public static WebSkladDbContext CreateContext()
        {
            var options =
                new DbContextOptionsBuilder<WebSkladDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new WebSkladDbContext(options);
            context.Database.EnsureCreated();
            ProductsContextFactory.Create(context);

            return context;
        }

        public static void Destroy(WebSkladDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
