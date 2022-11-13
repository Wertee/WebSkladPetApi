using Domain.Entity;
using Infrastructure;

namespace Tests.Common
{
    public class CategoryContextFactory
    {

        public static Guid CategoryAId = Guid.NewGuid();
        public static Guid CategoryBId = Guid.NewGuid();

        public static Guid CategoryIdForDelete = Guid.NewGuid();
        public static Guid CategoryIdForUpdate = Guid.NewGuid();

        public static void Create(WebSkladDbContext context)
        {
            context.Categories.AddRange(
                new Category()
                {
                    Id = Guid.Parse("DE8F25E4-E5BE-4982-A7C2-BF8EDFDCA01B"),
                    Name = "Mouse"
                },
                new Category()
                {
                    Id = Guid.Parse("5502E6E8-02CB-43C2-B777-8AB395FEBCC9"),
                    Name = "Keyboard"
                }
                );
            context.SaveChanges();
        }
    }
}
