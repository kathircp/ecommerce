using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    
    [Table("IndexPage", Schema = "ecomm")]
    public class IndexPage
    {
        [Key]
        public int Id { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string? FilterById { get; set; } = string.Empty;
        public int Rank { get; set; }

    }
}
