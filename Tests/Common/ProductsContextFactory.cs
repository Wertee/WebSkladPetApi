using Domain.Entity;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Tests.Common
{
    public class ProductsContextFactory
    {
        public static Guid ProductAId = Guid.NewGuid();
        public static Guid ProductBId = Guid.NewGuid();

        public static Guid ProductIdForDelete = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119");
        public static Guid ProductIdForUpdate = Guid.Parse("E8582C8E-3099-487D-9AC8-B30E9A40FF30");

        public static void Create(WebSkladDbContext context)
        {
            context.Products.AddRange(
                new Product()
                {
                    CanBeGiven = true,
                    CategoryId = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                    Count = 5,
                    Description = "Мышь Оклик",
                    Id = Guid.Parse("F02A40F3-F869-43E9-83E0-9F6396B8E119"),
                    Name = "Мышь"
                },
                new Product()
                {
                    CanBeGiven = true,
                    CategoryId = Guid.Parse("5502E6E8-02CB-43C2-B777-8AB395FEBCC9"),
                    Count = 5,
                    Description = "Клавиатура Оклик",
                    Id = Guid.Parse("E8582C8E-3099-487D-9AC8-B30E9A40FF30"),
                    Name = "Клавиатура"
                }
                );
            context.SaveChanges();

        }
    }
}
