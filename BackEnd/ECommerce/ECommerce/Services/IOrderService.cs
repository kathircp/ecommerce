using ECommerce.DTOs;

namespace ECommerce.Services
{
    public interface IOrderService
    {
        List<OrderCreateDto> GetAll();
        OrderCreateDto? Get(int id);
        OrderCreateDto? GetByCustomerId(int customerId);
        bool Create(OrderCreateDto userDetailDto);
        bool CreateOrder(OrderDto itemDto);
    }
}
