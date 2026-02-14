using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.DTOs;

namespace ECommerce.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll(int limit);

        Product? Get(int id);

        bool Create(Product product);

        bool Update(Product product);

        bool Delete(int id);

        int? FindCategoryByName(string categoryName);

        int UploadImage(FileStorage fileStorage);

        Task<FileStorage?> DownloadImage(int id);
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

        public bool Create(Product product)
        {
            _db.Products.Add(product);
            int count = _db.SaveChanges();
            return count > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            var p = _db.Products.Find(id);
            if (p == null) return false;
            _db.Products.Remove(p);
            _db.SaveChanges();
            return true;
        }

        public int? FindCategoryByName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return 0;

            try
            {
                var name = categoryName.Trim().ToLowerInvariant();
                //var category = _db.Categories.Where(x => x.CategoryName == categoryName)?.FirstOrDefault();

                var category = _db.Categories.Where(x => !string.IsNullOrEmpty(x.CategoryName) && x.CategoryName.ToLower() == name)?.FirstOrDefault();

                // Return 0 when not found (caller should treat 0 as "not found")
                return category?.Id ?? 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while finding category by name: {CategoryName}", categoryName);
                return 0;
            }
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

        public int UploadImage(FileStorage fileStorage)
        {
            _db.FileStorages.Add(fileStorage);
            int count = _db.SaveChanges();
            return count > 0 ? fileStorage.Id : 0;
        }
        public async Task<FileStorage?> DownloadImage(int id)
        {
            try
            {
                var file = await _db.FileStorages.FindAsync(id);
                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while downloading image with ID: {FileId}", id);
                return null;
            }
        }
    }
}