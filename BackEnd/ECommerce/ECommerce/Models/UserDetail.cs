using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("UserDetail", Schema = "ecomm")]
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Firstname { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;  
        public string AlterNamePhoneNo { get; set; } = string.Empty;
    }
}
