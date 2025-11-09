using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;

namespace ECommerce.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll(int limit);

        Product? Get(int id);

        Product Create(Product product);

        bool Update(Product product);

        bool Delete(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _db;
        private readonly ILogger _logger;

        public ProductRepository(ECommerceDbContext db, ILogger<ProductRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Product Create(Product product)
        {           
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

        public async Task<IEnumerable<Product>> GetAll(int limit)
        {
            try
            {   
                var products = await _db.Products.Take(limit).ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");                
            } 
            return Enumerable.Empty<Product>();
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