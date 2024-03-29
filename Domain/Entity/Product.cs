﻿namespace Domain.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeGiven { get; set; }
        public int Count { get; set; }
        public int CountToGive { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
