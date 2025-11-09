using ECommerce.Data;
using ECommerce.DTOs;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace ECommerce.Repositories
{
    public interface IIndexPageRepository
    {
        IEnumerable<IndexPage> GetAll();

        IndexPage? Get(int id);

        IndexPage Create(IndexPage indexPage);

        Task<List<PageDto>> GetPageInfoAsync();

        Task<List<FilterBy>> GetFilters(List<int> filterIds);
        Task<List<FilterBy>> GetAllFilters();
    }
    public class IndexPageRepository : IIndexPageRepository
    {
        private readonly ECommerceDbContext _db;
        private readonly ILogger _logger;

        public IndexPageRepository(ECommerceDbContext db, ILogger<IndexPageRepository> logger)
        {
            _db = db;
            _logger = logger;
           
        }

        public IEnumerable<IndexPage> GetAll()
        {
            return _db.IndexPages.ToList();
        }

        public IndexPage? Get(int id)
        {

            return _db.IndexPages.Find(id);
        }

        public IndexPage Create(IndexPage indexPage)
        {   
            _db.IndexPages.Add(indexPage);
            _db.SaveChanges();
            return indexPage;
        }

        public async Task<List<PageDto>> GetPageInfoAsync()
        {
            var response = new List<PageDto>();
            try
            {
                var result = _db.IndexPages
                .GroupJoin(
                    _db.Categories,
                    p => p.Id, // Inner key selector                    
                    c => c.IndexPageId, // Outer key selector
                    (p, categories) => new { Page = p, Categories = categories } // Result selector (intermediate anonymous type)
                )
                .SelectMany(
                    p_c => p_c.Categories.DefaultIfEmpty(), // Flatten the joined categories, handling nulls for left join
                    (p_c, c) => new // Result selector (final anonymous type)
                    {
                        p_c.Page.ModuleName,
                        p_c.Page.FilterById,
                        CategoryId = c != null ? (int?)c.Id : null, // Handle potentially null category ID
                        CategoryName = c != null ? c.CategoryName : null, // Handle potentially null category name
                        p_c.Page.Rank
                    }
                )
                .OrderBy(item => item.Rank); // Order by the Rank column

                
                foreach (dynamic res in result)
                {
                    response.Add(new PageDto
                    {
                        ModuleName = res.ModuleName,
                        FilterById = res.FilterById,
                        CategoryId = res.CategoryId ?? 0,
                        CategoryName = res.CategoryName,
                        Rank = res.Rank
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");                
            }
            return response;

        }

        public async Task<List<FilterBy>> GetFilters(List<int> filterIds)
        {
            return await _db.Filters.Where(x => filterIds.Contains(x.Id)).ToListAsync();
        }
        public async Task<List<FilterBy>> GetAllFilters()
        {
            return await _db.Filters.ToListAsync();
        }
    }
}