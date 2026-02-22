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
        private readonly string _uploadFolderPath;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
            // Save files in "Uploads" folder inside wwwroot
            //_uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            //// Create folder if it doesn't exist
            //if (!Directory.Exists(_uploadFolderPath))
            //{
            //    Directory.CreateDirectory(_uploadFolderPath);
            //}
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
        [HttpPost]
        public async Task<IActionResult> Upsert(ProductCreateDto productCreateDto)
        {
            bool isValid = false;
            ProductDto? exitingproduct = null;
            if (productCreateDto == null)
            {
                return BadRequest("Invalid product data or image.");
            }
            if (productCreateDto.Price < 0 || productCreateDto.Stock < 0 || productCreateDto.Discount < 0)
            {
                return BadRequest("Price, Stock, and Discount must be non-negative.");
            }
            if (productCreateDto?.Id > 0)
            {
                exitingproduct = await _productService.Get(productCreateDto.Id);
                if (exitingproduct != null)
                {
                    using var memoryStream = new MemoryStream();
                    await productCreateDto.Image.CopyToAsync(memoryStream);

                    var fileEntity = new FileStorageDto
                    {
                        Id = Convert.ToInt32(exitingproduct.ImageUrl),
                        FileName = productCreateDto.Image.FileName,
                        ContentType = productCreateDto.Image.ContentType,
                        FileData = memoryStream.ToArray()
                    };
                    int fileId = _productService.UpdateImage(fileEntity);

                    ProductDto productDto = new ProductDto
                    {
                        Id = exitingproduct.Id,
                        Name = productCreateDto.Name,
                        Description = productCreateDto.Description,
                        Price = productCreateDto.Price,
                        Stock = productCreateDto.Stock,
                        CategoryId = _productService.FindCategoryByName(productCreateDto.CategoryName),
                        Color = productCreateDto.Color,
                        Discount = productCreateDto.Discount,
                        Blouse = productCreateDto.IncludeBlouse,
                        ImageUrl = fileId.ToString(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = productCreateDto.UpdatedBy,
                        NewArrival = true
                    };
                    isValid = await _productService.Update(productDto);
                }
            }
            else
            {             
                using var memoryStream = new MemoryStream();
                await productCreateDto.Image.CopyToAsync(memoryStream);

                var fileEntity = new FileStorageDto
                {
                    FileName = productCreateDto.Image.FileName,
                    ContentType = productCreateDto.Image.ContentType,
                    FileData = memoryStream.ToArray()
                };
                int fileId = _productService.UploadImage(fileEntity);
                if (fileId == 0)
                {
                    return StatusCode(500, "Failed to upload image.");
                }
                ProductDto productDto = new ProductDto
                {
                    Name = productCreateDto.Name,
                    Description = productCreateDto.Description,
                    Price = productCreateDto.Price,
                    Stock = productCreateDto.Stock,
                    CategoryId = _productService.FindCategoryByName(productCreateDto.CategoryName),
                    Color = productCreateDto.Color,
                    Discount = productCreateDto.Discount,
                    Blouse = productCreateDto.IncludeBlouse,
                    ImageUrl = fileId.ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = productCreateDto.UpdatedBy,
                    NewArrival = true
                };
                isValid = await _productService.Create(productDto);
            }
            return Ok(isValid);
        }        
    }
}