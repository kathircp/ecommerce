using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Shipment", Schema = "ecomm")]
    public class Shipment
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int DeliveryAddressId { get; set; }
        public string Status { get; set; } = null!;
        public decimal OrderAmount { get; set; }
        public decimal ShipAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;       
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;

    }
}
