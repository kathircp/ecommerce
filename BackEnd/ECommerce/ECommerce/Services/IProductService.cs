using ECommerce.DTOs;

namespace ECommerce.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsByPageAsync(int limit);
        Task<ProductDto?> Get(int id);
    }
}
