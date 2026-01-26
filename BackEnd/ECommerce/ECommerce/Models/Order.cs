using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ECommerce.Models
{
    [Table("Orders", Schema = "ecomm")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;

    }
}