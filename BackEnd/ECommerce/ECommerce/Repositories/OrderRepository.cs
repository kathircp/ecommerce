using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order? Get(int id);

        Order Create(Order order);
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _db;

        public OrderRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> GetAll()
        {
            return _db.Orders.ToList();
        }

        public Order? Get(int id)
        {

            return _db.Orders.Find(id);
        }

        public Order Create(Order order)
        {
            //order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }
    }
}