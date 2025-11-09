using ECommerce.DTOs;

namespace ECommerce.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsByPageAsync();
    }
}
