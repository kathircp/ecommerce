# File Upload Backend Implementation Guide

## Frontend Implementation ✅
The frontend is now set up to:
- Accept image files (JPEG, PNG, GIF, WebP)
- Validate file size (max 5MB)
- Show image preview before upload
- Display file name
- Show error messages
- Display loading state during submission

---

## Backend Implementation (C# Example)

### 1. Create a File Upload Service
Create `Services/FileUploadService.cs`:

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace YourNamespace.Services
{
    public interface IFileUploadService
    {
        Task<string> SaveProductImageAsync(IFormFile file, string productName);
        void DeleteProductImage(string fileName);
    }

    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;
        private const string UPLOAD_FOLDER = "assets/ProductImages";
        private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB

        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveProductImageAsync(IFormFile file, string productName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            // Validate file
            if (file.Length > MAX_FILE_SIZE)
                throw new ArgumentException("File size exceeds maximum limit of 5MB");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            
            if (!Array.Exists(allowedExtensions, ext => ext == fileExtension))
                throw new ArgumentException("Invalid file type. Only image files are allowed");

            // Create upload directory if it doesn't exist
            var uploadPath = Path.Combine(_environment.WebRootPath, UPLOAD_FOLDER);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Generate unique filename
            var uniqueFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for database storage
            return Path.Combine(UPLOAD_FOLDER, uniqueFileName).Replace("\\", "/");
        }

        public void DeleteProductImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var filePath = Path.Combine(_environment.WebRootPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
```

### 2. Register Service in Startup
In `Program.cs`:

```csharp
// Add to services
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
```

### 3. Update Product Controller
```csharp
[HttpPost]
public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto)
{
    try
    {
        string imagePath = null;

        // Handle file upload
        if (dto.Image != null && dto.Image.Length > 0)
        {
            imagePath = await _fileUploadService.SaveProductImageAsync(dto.Image, dto.Name);
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = dto.CategoryId,
            Color = dto.Color,
            Discount = dto.Discount,
            ImagePath = imagePath, // Store relative path
            CreatedAt = DateTime.Parse(dto.CreatedAt)
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "Product created successfully", imagePath });
    }
    catch (ArgumentException ex)
    {
        return BadRequest(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "An error occurred while saving the product" });
    }
}
```

### 4. Create DTO
```csharp
public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public string Color { get; set; }
    public decimal? Discount { get; set; }
    public IFormFile Image { get; set; }
    public string CreatedAt { get; set; }
}
```

### 5. Folder Structure
Ensure your folder structure includes:
```
wwwroot/
  assets/
    ProductImages/  (will be created automatically)
    PhotoGallery/
    ...
```

---

## Key Points
- ✅ All validation happens on both frontend and backend
- ✅ Files are saved to `wwwroot/assets/ProductImages/`
- ✅ Unique filenames prevent overwrites
- ✅ Relative paths stored in database
- ✅ Error handling for file operations
- ✅ Maximum file size enforced (5MB)
- ✅ Only image files allowed (JPEG, PNG, GIF, WebP)
