using AutoMapper;
using ECommerce.DTOs;
using ECommerce.Repositories;

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
            return dtoResponse;
        }
        public async Task<ProductDto?> Get(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null) return null;
            var dtoResponse = _mapper.Map<ProductDto>(product);
            return dtoResponse;
        }
    }
}
