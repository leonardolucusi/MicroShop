using AutoMapper;
using MicroShop.ProductAPI.Application.Interfaces;
using MicroShop.ProductAPI.Domain.DTOs;
using MicroShop.ProductAPI.Domain.Entities;
using MicroShop.ProductAPI.Domain.Repositories;
namespace MicroShop.ProductAPI.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IQueryable<ProductDTO>> GetProductsAsync()
        {
            return _mapper.ProjectTo<ProductDTO>(await _productRepository.GetAllAsync());
        }
        public async Task<ProductDTO> GetProductByIdAsync(Ulid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductDTO>(product);
        }
        public async Task<ProductDTO> CreateProductAsync(CreateProductDTO createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.Id = Ulid.NewUlid();
            await _productRepository.AddAsync(product);

            return _mapper.Map<ProductDTO>(product);
        }
        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var product = await _productRepository.GetByIdAsync(productDto.Id);
            if (product == null) return;

            _mapper.Map(productDto, product);

            await _productRepository.UpdateAsync(product);
        }
        public async Task DeleteProductAsync(Ulid id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
