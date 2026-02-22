using AutoMapper;
using Azure.Core;
using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<List<ProductDto>> GetProductsByPageAsync(int limit)
        {
            var repoResponse = await _productRepository.GetAll(limit);
            var dtoResponse = _mapper.Map<List<ProductDto>>(repoResponse);
            foreach (var dtoRes in dtoResponse)
            {
                var fileStorage = await _productRepository.DownloadImage(Convert.ToInt32(dtoRes.ImageUrl));
                dtoRes.Image = _mapper.Map<FileStorageDto>(fileStorage); 
            }
            return dtoResponse;
        }
        public async Task<ProductDto?> Get(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null) return null;
            var dtoResponse = _mapper.Map<ProductDto>(product);
            var fileStorage = await _productRepository.DownloadImage(Convert.ToInt32(dtoResponse.ImageUrl));
            dtoResponse.Image = _mapper.Map<FileStorageDto>(fileStorage);
            return dtoResponse;
        }
        public async Task<bool> Create(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = _productRepository.Create(product);
            return createdProduct;
        }
        public async Task<bool> Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = _productRepository.Update(product);
            return createdProduct;
        }

        public int? FindCategoryByName(string categoryName)
        {
            return _productRepository.FindCategoryByName(categoryName);
        }

        public int UploadImage(FileStorageDto fileStorageDto)
        {
            var fileStorage = _mapper.Map<FileStorage>(fileStorageDto);
            return _productRepository.UploadImage(fileStorage);
        }
        public int UpdateImage(FileStorageDto fileStorageDto)
        {
            var fileStorage = _mapper.Map<FileStorage>(fileStorageDto);
            return _productRepository.UpdateImage(fileStorage);
        }
        public async Task<FileStorageDto> DownloadImage(int id)
        {
            return _mapper.Map<FileStorageDto>(await _productRepository.DownloadImage(id));
        }
    }
}
