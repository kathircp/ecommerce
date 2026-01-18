using ECommerce.Data;
using ECommerce.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);   
        
    }

    public class UserRepository : IUserRepository
    {
        private readonly ECommerceDbContext _db;
      
        public UserRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public User? GetByUsername(string userName)
        {
            return _db.Users.Where(x=> x.Username.ToUpper() == userName.ToUpper())?.FirstOrDefault();
        }

    }
}