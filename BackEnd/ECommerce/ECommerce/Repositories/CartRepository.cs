using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetAll();

        Cart? Get(int id);

        Cart Create(Cart cart);
    }
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDbContext _db;

        public CartRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Cart> GetAll()
        {
            return _db.Carts.ToList();
        }

        public Cart? Get(int id)
        {

            return _db.Carts.Find(id);
        }

        public Cart Create(Cart cart)
        {  
            _db.Carts.Add(cart);
            _db.SaveChanges();
            return cart;
        }
    }
}