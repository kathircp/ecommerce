using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    public class OrderCreateDto
    {       
        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Total { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }

}