using System;
using System.Collections.Generic;

namespace ECommerce.DTOs
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal LineTotal { get; set; }
    }

    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();

        public decimal Total { get; set; }
    }

}