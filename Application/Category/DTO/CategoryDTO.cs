using System.ComponentModel.DataAnnotations;

namespace Application.Category.DTO
{
    public class CategoryDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
