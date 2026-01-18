using System;
using System.Collections.Generic;
using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IUserDetailRepository
    {
        IEnumerable<UserDetail> GetAll(int userid);

        UserDetail? GetByUserId(int userid);
        UserDetail? Get(int id);
        bool Create(UserDetail userDetail);
        bool UpdatePrimary(int id, int userId);
    }
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly ECommerceDbContext _db;
        public UserDetailRepository(ECommerceDbContext db)
        {
            _db = db;
        }
        public bool Create(UserDetail userDetail)
        {           
            _db.UserDetails.Add(userDetail);
            int count = _db.SaveChanges();            
            return count > 0 ?  true : false;
        }

        public UserDetail? GetByUserId(int userid)
        {
            return _db.UserDetails.Where(x=> x.UserId == userid && x.IsPrimary)?.FirstOrDefault();
        }
        public UserDetail? Get(int id)
        {
            return _db.UserDetails.Find(id);
        }

        public IEnumerable<UserDetail> GetAll(int userid)
        {
            return _db.UserDetails?.Where(x=> x.UserId == userid)?.ToList();
        }

        public bool UpdatePrimary(int id, int userId)
        {
            var userDetail = _db.UserDetails.Where(x => x.UserId == userId && x.Id != id);
            foreach (var detail in userDetail)
            {
                detail.IsPrimary = false;
            }
            _db.UserDetails.UpdateRange(userDetail);
            int count = _db.SaveChanges();
            return count > 0 ? true : false;                  
        }
    }
}