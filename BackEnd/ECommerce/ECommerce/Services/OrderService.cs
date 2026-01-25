using AutoMapper;
using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        //private readonly ILogger<OrderService> _logger;
        private readonly IUserDetailService _userDetailService;
        public OrderService(IMapper mapper, IOrderRepository orderRepository, 
            IOrderItemsRepository orderItemsRepository, IUserDetailService userDetailService) { 
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
            _userDetailService = userDetailService;
        }
        public bool Create(OrderCreateDto userDetailDto)
        {
            return _orderRepository.Create(_mapper.Map<OrderCreateDto, Models.Order>(userDetailDto));
        }
        public bool CreateItem(OrderDto itemDto)
        {
            //OrderCreateDto orderCreateDto = GetByCustomerId(itemDto.CustomerId);
            //OrderItem orderItem = _mapper.Map<OrderItemDto, Models.OrderItem>(itemDto);
            //return _orderItemsRepository.Create(orderItem);
            return true;
        }

        public OrderCreateDto? Get(int id)
        {
            return _mapper.Map<Models.Order, OrderCreateDto?>(_orderRepository.Get(id));
        }

        public List<OrderCreateDto> GetAll()
        {
            IEnumerable<Models.Order> orders = _orderRepository.GetAll();
            return _mapper.Map<List<Models.Order>, List<OrderCreateDto>>((List<Models.Order>)orders);
        }

        public OrderCreateDto? GetByCustomerId(int customerId)
        {
            return _mapper.Map<Models.Order, OrderCreateDto?>(_orderRepository.GetByCustomerId(customerId));
        }
    }
}
