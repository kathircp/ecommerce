using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Users", Schema = "ecom")]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!; // plaintext for demo only - do not use in production
        public List<string> Roles { get; set; } = new();
    }
}