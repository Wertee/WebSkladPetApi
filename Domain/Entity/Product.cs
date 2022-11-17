using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeGiven { get; set; }
        public int Count { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
