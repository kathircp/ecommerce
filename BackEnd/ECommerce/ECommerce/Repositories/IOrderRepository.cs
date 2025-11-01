using System;
using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order? Get(Guid id);

        Order Create(Order order);
    }
}