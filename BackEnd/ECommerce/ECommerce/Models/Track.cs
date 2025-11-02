using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Track", Schema = "ecomm")]
    public class Track
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int ShimentId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string PackageVendorName { get; set; } = null!;
        public string PackageVendorAddress { get; set; } = null!;
        public string PackageStatus { get; set; } = null!;
        public string DeliveryStatus { get; set; } = null!;
        public string DeliveryRemarks { get; set; } = null!;        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
    }
}
