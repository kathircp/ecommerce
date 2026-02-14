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
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            bool isValid = false;
            //(string fileUrl, string message, bool isUploaded) uploadResult = await UploadImage(productCreateDto.Image);
            //if (uploadResult.isUploaded)
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
                if (fileId == 0 )
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
        //private async Task<(string fileUrl, string messge, bool isUploaded)> UploadImage(IFormFile file)
        //{
        //    try
        //    {
        //        // Validate file
        //        if (file == null || file.Length == 0)
        //            return (string.Empty, "No file uploaded.", false);

        //        // Validate file type (only images)
        //        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        //        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        //        if (string.IsNullOrEmpty(extension) || Array.IndexOf(allowedExtensions, extension) < 0)
        //            return (file.FileName, "Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.", false);

        //        // Generate unique file name
        //        var fileName = $"{Guid.NewGuid()}{extension}";

        //        // Full path
        //        var filePath = Path.Combine(_uploadFolderPath, fileName);

        //        // Save file to folder
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        // Return file URL
        //        var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{fileName}";
        //        return (fileUrl,"File uploaded successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ("", $"500: Internal server error: {ex.Message}", false);
        //    }
        //}
    }
}