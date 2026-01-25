using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IOrderItemsRepository
    {
        IEnumerable<OrderItem> GetAll();

        OrderItem? Get(int id);

        bool Create(OrderItem order);
    }
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly ECommerceDbContext _db;
        public OrderItemsRepository(ECommerceDbContext db)
        {
            _db = db;
        }
        public bool Create(OrderItem orderItem)
        {
            _db.OrderItems.Add(orderItem);
            int count = _db.SaveChanges();
            return count > 0 ? true : false;
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
