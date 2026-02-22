namespace ECommerce.DTOs
{
    public class ProductCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; } = null!;
        public int Discount { get; set; }
        public bool IncludeBlouse { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public IFormFile Image { get; set; }
    }
}
