namespace ECommerce.DTOs
{
    public class UserDetailDto
    {
        public string UserName { get; set; } = null!;       
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string AlterNamePhoneNo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
    
}
