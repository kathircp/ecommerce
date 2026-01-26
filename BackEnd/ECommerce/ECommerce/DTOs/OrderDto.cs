using System;
using System.Collections.Generic;

namespace ECommerce.DTOs
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal LineTotal { get; set; }
    }
    public class PaymentDto
    {
        public string Mode { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string TransactionId { get; set; }
    }

    public class OrderDto
    {   
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string OrderStatus { get; set; } = null!;
        public string Remarks { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public UserDetailDto Address { get; set; } = null!;
        public PaymentDto Payment { get; set; } = null!;

    }

}