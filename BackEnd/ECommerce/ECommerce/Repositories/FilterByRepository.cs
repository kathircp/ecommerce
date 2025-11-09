using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IFilterByRepository
    {
        IEnumerable<FilterBy> GetAll();

        FilterBy? Get(int id);

        FilterBy Create(FilterBy filet);
    }
    public class FilterByRepository : IFilterByRepository
    {
        private readonly ECommerceDbContext _db;

        public FilterByRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public IEnumerable<FilterBy> GetAll()
        {
            return _db.Filters.ToList();
        }

        public FilterBy? Get(int id)
        {

            return _db.Filters.Find(id);
        }

        public FilterBy Create(FilterBy filterBy)
        {   
            _db.Filters.Add(filterBy);
            _db.SaveChanges();
            return filterBy;
        }
    }
}