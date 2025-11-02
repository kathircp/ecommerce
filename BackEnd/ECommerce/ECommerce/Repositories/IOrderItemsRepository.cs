using System;
using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IOrderItemsRepository
    {
        IEnumerable<OrderItem> GetAll();

        OrderItem? Get(int id);

        OrderItem Create(OrderItem product);

        bool Update(OrderItem product);

        bool Delete(int id);
    }
}