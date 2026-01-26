using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Repositories;
using ECommerce.DTOs;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Services;

namespace ECommerce.Controllers
{    
    [ApiController]
    [Route("api/ecommerce/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;        

        public OrdersController(IOrderService orderSerivice)
        {
            _orderService = orderSerivice;           
        }

        [HttpGet]       
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = _orderService.Get(id);
            if (order == null) return NotFound();            
            return Ok(order);
        }

        [HttpPost]        
        public IActionResult Create([FromBody] OrderCreateDto create)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(_orderService.Create(create));           
        }
        [HttpPost ("CreateOrderItem")]
        public IActionResult CreateOrder([FromBody] OrderDto itemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(_orderService.CreateOrder(itemDto));
        }        

    }
}