using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;

namespace ECommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _db;

        public ProductRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public Product Create(Product product)
        {
            //product.Id = Guid.NewGuid();
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var p = _db.Products.Find(id);
            if (p == null) return false;
            _db.Products.Remove(p);
            _db.SaveChanges();
            return true;
        }

        public Product? Get(int id)
        {
            return _db.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.AsNoTracking().ToList();
        }

        public bool Update(Product product)
        {
            var existing = _db.Products.Find(product.Id);
            if (existing == null) return false;
            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            _db.SaveChanges();
            return true;
        }
    }
}