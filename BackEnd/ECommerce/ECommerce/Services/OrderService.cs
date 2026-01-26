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
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;
        public OrderService(IMapper mapper, IOrderRepository orderRepository, 
            IOrderItemsRepository orderItemsRepository, IUserDetailRepository userDetailRepository,
            IUserRepository userRepository, IPaymentRepository paymentRepository) { 
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
            _userDetailRepository = userDetailRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
        }
        public bool Create(OrderCreateDto userDetailDto)
        {
            return _orderRepository.Create(_mapper.Map<OrderCreateDto, Models.Order>(userDetailDto));
        }
        public bool CreateOrder(OrderDto itemDto)
        {      
            OrderCreateDto createOrder = _mapper.Map<OrderDto, OrderCreateDto>(itemDto);
            var userInfo = _userRepository.GetByUsername(itemDto.UserName);
            createOrder.CustomerId = userInfo.Id;
            Order order = _mapper.Map<OrderCreateDto, Models.Order>(createOrder);
            bool isValid =  _orderRepository.Create(order);
            if (isValid)
            {
                List<OrderItem> orderItems = _mapper.Map<List<OrderItemDto>, List<OrderItem>>(itemDto.Items);
                foreach (var orderItem in orderItems)
                {
                    orderItem.OrderId = order.Id;
                }
                isValid = _orderItemsRepository.Create(orderItems);
                if (isValid)
                {
                    
                    UserDetail userDetail = _mapper.Map<UserDetailDto, UserDetail>(itemDto.Address);
                    userDetail.UserId = userInfo.Id;
                    isValid = _userDetailRepository.Create(userDetail);
                    
                    if (isValid)
                    {
                        Payment payment = _mapper.Map<PaymentDto, Payment>(itemDto.Payment);
                        payment.OrderId = order.Id;
                        payment.CustomerId = userInfo.Id;
                        payment.Currency = "INR";
                        payment.TransactionDesc = "Testing";
                        payment.Amount = order.Total;
                        isValid = _paymentRepository.Create(payment);
                    }
                }
            }
            return isValid;
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
