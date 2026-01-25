using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    public class OrderCreateDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Total { get; set; }
    }

}