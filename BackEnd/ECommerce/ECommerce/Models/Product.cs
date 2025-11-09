using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Products", Schema = "ecomm")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }        
        public int CategoryId { get; set; }
        public string? Color { get; set; } = string.Empty;
        public int Discount { get; set; } //in percentage
        public bool Blouse { get; set; }
        public bool NewArrival { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; } = string.Empty;
    }
}