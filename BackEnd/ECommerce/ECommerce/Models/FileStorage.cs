using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("FileStorage", Schema = "ecomm")]
    public class FileStorage
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = Array.Empty<byte>();
    }
    
}
