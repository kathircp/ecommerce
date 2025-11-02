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

        //public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        //public decimal Total => Items.Sum(i => i.LineTotal);
    }
}