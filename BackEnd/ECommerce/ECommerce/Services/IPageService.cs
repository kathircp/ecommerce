using ECommerce.DTOs;

namespace ECommerce.Services
{
    public interface IPageService
    {
        Task<List<PageDto>> GetPageInfoAsync();        
    }
}
