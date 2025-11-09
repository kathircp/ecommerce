using System;
using System.Collections.Generic;
using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IUserDetailRepository
    {
        IEnumerable<UserDetail> GetAll();

        UserDetail? Get(int id);

        UserDetail Create(UserDetail userDetail);
    }
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly ECommerceDbContext _db;
        public UserDetailRepository(ECommerceDbContext db)
        {
            _db = db;
        }
        public UserDetail Create(UserDetail userDetail)
        {           
            _db.UserDetails.Add(userDetail);
            _db.SaveChanges();
            return userDetail;
        }

        public UserDetail? Get(int id)
        {
            return _db.UserDetails.Find(id);
        }

        public IEnumerable<UserDetail> GetAll()
        {
            return _db.UserDetails.ToList();
        }
    }
}