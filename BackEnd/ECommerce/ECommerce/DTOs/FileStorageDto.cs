namespace ECommerce.DTOs
{
    public class FileStorageDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = Array.Empty<byte>();
    }
    
}
