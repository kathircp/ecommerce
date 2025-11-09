namespace ECommerce.DTOs
{
    public class PageDto
    {
        public string? ModuleName { get; set; }
        public string? FilterById { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int Rank { get; set; }
        public List<FilterDto> Filters { get; set; } = new();
    }
    public class FilterDto
    {
        public int Id { get; set; }
        public string Filtername { get; set; } = null!;
        public string OptionName { get; set; } = null!;
        public string OptionValue { get; set; } = null!;
    }
}
