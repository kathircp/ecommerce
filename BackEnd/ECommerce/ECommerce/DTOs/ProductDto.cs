using System;

namespace ECommerce.DTOs
{
    public class ProductDto
    {        
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
        public string Color { get; set; } = null!;
        public int Discount { get; set; }
        public bool Blouse { get; set; }
        public bool NewArrival { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public FileStorageDto? Image { get; set; }

    }
}