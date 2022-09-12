using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Category.DTO;

namespace Application.Product.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeGiven { get; set; }
        public int Count { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDTO CategoryDTO { get; set; }
    }
}
