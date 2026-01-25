using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Repositories;
using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Services;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/ecommerce/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit)
        {
            return Ok(await _productService.GetProductsByPageAsync(limit));
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.Get(id));           
        }
    }
}