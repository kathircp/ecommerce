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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetProductsByPageAsync());
        }


        //[HttpGet("{id:int}")]
        //public IActionResult Get(int id)
        //{
        //    var p = _products.Get(id);
        //    if (p == null) return NotFound();
        //    var dto = new ProductDto
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        Description = p.Description,
        //        Price = p.Price,
        //        Stock = p.Stock
        //    };
        //    return Ok(dto);
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody] ProductCreateDto dto)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var product = new Product
        //    {
        //        Name = dto.Name,
        //        Description = dto.Description,
        //        Price = dto.Price,
        //        Stock = dto.Stock
        //    };

        //    var created = _products.Create(product);

        //    var result = new ProductDto
        //    {
        //        Id = created.Id,
        //        Name = created.Name,
        //        Description = created.Description,
        //        Price = created.Price,
        //        Stock = created.Stock
        //    };

        //    return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        //}

        //[HttpPut("{id:int}")]
        //public IActionResult Update(int id, [FromBody] ProductUpdateDto dto)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var existing = _products.Get(id);
        //    if (existing == null) return NotFound();

        //    existing.Name = dto.Name;
        //    existing.Description = dto.Description;
        //    existing.Price = dto.Price;
        //    existing.Stock = dto.Stock;

        //    var ok = _products.Update(existing);
        //    if (!ok) return StatusCode(500, "Unable to update product.");

        //    return NoContent();
        //}

        //[HttpDelete("{id:int}")]
        //public IActionResult Delete(int id)
        //{
        //    var existing = _products.Get(id);
        //    if (existing == null) return NotFound();

        //    var ok = _products.Delete(id);
        //    if (!ok) return StatusCode(500, "Unable to delete product.");

        //    return NoContent();
        //}
    }
}