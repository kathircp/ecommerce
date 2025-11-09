using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IOrderItemsRepository
    {
        IEnumerable<OrderItem> GetAll();

        OrderItem? Get(int id);

        OrderItem Create(OrderItem order);
    }
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly ECommerceDbContext _db;
        public OrderItemsRepository(ECommerceDbContext db)
        {
            _db = db;
        }
        public OrderItem Create(OrderItem orderItem)
        {
            _db.OrderItems.Add(orderItem);
            _db.SaveChanges();
            return orderItem;
        }
       
        public OrderItem? Get(int id)
        {
            return _db.OrderItems.Find(id);
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _db.OrderItems.AsNoTracking().ToList();
        }

       
    }
}
