using ECommerce.DTOs;

namespace ECommerce.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsByPageAsync(int limit);
        Task<ProductDto?> Get(int id);
        Task<bool> Create(ProductDto productDto);
        int? FindCategoryByName(string categoryName);
        int UploadImage(FileStorageDto fileStorage);
        Task<FileStorageDto> DownloadImage(int id);
    }
}
