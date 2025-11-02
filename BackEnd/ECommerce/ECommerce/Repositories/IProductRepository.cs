using System;
using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product? Get(int id);

        Product Create(Product product);

        bool Update(Product product);

        bool Delete(int id);
    }
}