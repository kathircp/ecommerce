using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order? Get(int id);
        Order? GetByCustomerId(int customerId);

        bool Create(Order order);

        IEnumerable<OrderItem> GetOrdersByOrderId(int orderId);
        Task<IEnumerable<OrderItem>> GetOrdersByCustomerId(int customerId);
        OrderItem? GetOrderItem(int id);
        bool CreateOrderItem(OrderItem order);
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

        public bool Create(Order order)
        {           
            order.CreatedAt = DateTime.UtcNow;
            _db.Orders.Add(order);
            int count = _db.SaveChanges();
            return count > 0 ? true : false;
        }

        public Order? GetByCustomerId(int customerId)
        {
            return _db.Orders.Where(x => x.CustomerId == customerId).FirstOrDefault();
        }

        public IEnumerable<OrderItem> GetOrdersByOrderId(int orderId)
        {
            return _db.OrderItems.Where(x => x.OrderId == orderId).ToList();
        }

        //public async Task<IEnumerable<OrderItemList>> GetOrdersByCustomerId(int customerId)
        //{
        //    var result = await _db.Orders
        //    .Select(o => new
        //    {
        //        OrderId = o.Id,
        //        CustomerId = o.CustomerId,
        //        CreatedAt = o.CreatedAt,
        //        Total = o.Total,
        //        Items = _db.OrderItems
        //            .Where(oi => oi.OrderId == o.Id)
        //            .Select(oi => new
        //            {
        //                oi.Id,
        //                oi.OrderId,
        //                oi.ProductId,
        //                oi.ProductName,
        //                oi.UnitPrice,
        //                oi.Quantity,
        //                oi.LineTotal
        //            })
        //            .ToList()
        //    })
        //    .ToListAsync();

        //    return (IEnumerable<OrderItemList>)result;

        //}
        public async Task<IEnumerable<OrderItem>>? GetOrdersByCustomerId(int customerId)
        {
            var result = await _db.Orders
            .Where(o => o.CustomerId == customerId)
            .Join(
                _db.OrderItems,
                o => o.Id,
                oi => oi.OrderId,
                (o, oi) => new
                {
                    oi.OrderId,
                    oi.ProductId,
                    oi.ProductName,
                    oi.UnitPrice,
                    oi.Quantity,
                    oi.LineTotal
                }).ToListAsync();            

            return (IEnumerable<OrderItem>)result;

        }

        public OrderItem? GetOrderItem(int id)
        {
            return _db.OrderItems.Find(id);
        }

        public bool CreateOrderItem(OrderItem orderItem)
        {  
            _db.OrderItems.Add(orderItem);
            int count = _db.SaveChanges();
            return count > 0 ? true : false;
        }

        
    }
}