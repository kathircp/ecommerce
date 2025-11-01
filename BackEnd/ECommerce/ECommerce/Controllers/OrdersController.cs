using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Repositories;
using ECommerce.DTOs;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orders;
        private readonly IProductRepository _products;

        public OrdersController(IOrderRepository orders, IProductRepository products)
        {
            _orders = orders;
            _products = products;
        }

        [HttpGet]       
        public IActionResult GetAll()
        {
            var dtos = _orders.GetAll().Select(o => new OrderDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    LineTotal = i.LineTotal
                }).ToList(),
                Total = o.Total
            });
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var o = _orders.Get(id);
            if (o == null) return NotFound();
            var dto = new OrderDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    LineTotal = i.LineTotal
                }).ToList(),
                Total = o.Total
            };
            return Ok(dto);
        }

        [HttpPost]        
        public IActionResult Create([FromBody] OrderCreateDto create)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (create.Items == null || !create.Items.Any()) return BadRequest("Order must contain at least one item.");

            // Validate products and stock
            var items = create.Items.Select(li =>
            {
                var product = _products.Get(li.ProductId);
                if (product == null) throw new ArgumentException($"Product not found: {li.ProductId}");
                if (product.Stock < li.Quantity) throw new InvalidOperationException($"Insufficient stock for product {product.Name}");
                return new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = li.Quantity
                };
            }).ToList();

            // Deduct stock (simple approach)
            foreach (var it in items)
            {
                var prod = _products.Get(it.ProductId)!;
                prod.Stock -= it.Quantity;
                _products.Update(prod);
            }

            var order = new Order
            {
                Items = items
            };

            var created = _orders.Create(order);

            var dto = new OrderDto
            {
                Id = created.Id,
                CreatedAt = created.CreatedAt,
                Items = created.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    LineTotal = i.LineTotal
                }).ToList(),
                Total = created.Total
            };

            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }
    }
}