using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Category", Schema = "ecomm")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int IndexPageId { get; set; }    
        public string? CategoryName { get; set; } = string.Empty;
        public int SubCategoryId { get; set; }
        public int Rank { get; set; }       
    }
}
