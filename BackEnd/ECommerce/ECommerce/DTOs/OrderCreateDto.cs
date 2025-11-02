using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    public class OrderLineCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }

    public class OrderCreateDto
    {
        [Required]
        public List<OrderLineCreateDto> Items { get; set; } = new();
    }
}