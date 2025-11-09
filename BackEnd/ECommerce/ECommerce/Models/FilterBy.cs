using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("FilterBy", Schema = "ecomm")]
    public class FilterBy
    {
        [Key]
        public int Id { get; set; }       
        public string? FilterName { get; set; } = string.Empty;
        public string? OptionName { get; set; } = string.Empty;
        public string? OptionValue  { get; set; } = string.Empty;

    }
}
