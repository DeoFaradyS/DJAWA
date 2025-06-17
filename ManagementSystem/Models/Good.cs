using System;

namespace ManagementSystem.Models
{
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Good()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Good: {Name} (Qty: {Quantity}, Price: {Price:C})";
        }

        public Good(Good other)
        {
            if (other == null) return;

            Id = other.Id;
            Name = other.Name;
            Description = other.Description;
            Quantity = other.Quantity;
            Price = other.Price;
            CreatedAt = other.CreatedAt;
            UpdatedAt = other.UpdatedAt;
        }

        public Good Clone()
        {
            return new Good(this);
        }
    }
}