using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    //public interface ICategoryRepository
    //{
    //    IEnumerable<Category> GetAll();

    //    Category? Get(int id);

    //    Category Create(Category category);
    //}
    //public class CategoryRepository : ICategoryRepository
    //{
    //    private readonly ECommerceDbContext _db;

    //    public CategoryRepository(ECommerceDbContext db)
    //    {
    //        _db = db;
    //    }

    //    public IEnumerable<Category> GetAll()
    //    {
    //        return _db.Categories.ToList();
    //    }

    //    public Category? Get(int id)
    //    {

    //        return _db.Categories.Find(id);
    //    }

    //    public Category Create(Category category)
    //    {   
    //        _db.Categories.Add(category);
    //        _db.SaveChanges();
    //        return category;
    //    }
    //}
}