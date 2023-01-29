using System.ComponentModel.DataAnnotations;

namespace Application.Product.DTO
{
    public class ProductDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; }
        [Required]
        public bool CanBeGiven { get; set; }
        [Required]
        [Range(0, 999)]
        public int Count { get; set; }
        public int CountToGive { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
