using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _db;

        public InMemoryOrderRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> GetAll()
        {
            return _db.Orders.Include(o => o.Items).AsNoTracking().ToList();
        }

        public Order? Get(Guid id)
        {
            return _db.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id);
        }

        public Order Create(Order order)
        {
            order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }
    }
}