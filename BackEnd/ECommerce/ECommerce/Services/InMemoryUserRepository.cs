using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models;

namespace ECommerce.Services
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
    }

    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);

        public InMemoryUserRepository()
        {
            // Demo users - passwords are plain text for simplicity (do NOT use in production)
            var admin = new User { Username = "admin", Password = "admin123", Roles = new List<string> { "Admin" } };
            var user = new User { Username = "user", Password = "user123", Roles = new List<string> { "User" } };
            _users[admin.Username] = admin;
            _users[user.Username] = user;
        }

        public User? GetByUsername(string username)
        {
            _users.TryGetValue(username, out var u);
            return u;
        }
    }
}