namespace ECommerce.Models
{
    public class OrderItemList
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
