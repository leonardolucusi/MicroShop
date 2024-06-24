using MicroShop.ProductAPI.Domain.DTOs;

namespace MicroShop.ProductAPI.Application.Interfaces
{
    public interface IProductService
    {
        public Task<IQueryable<ProductDTO>> GetProductsAsync();
        public Task<ProductDTO> GetProductByIdAsync(Ulid id);
        public Task<ProductDTO> CreateProductAsync(CreateProductDTO productDto);
        public Task UpdateProductAsync(ProductDTO productDto);
        public Task DeleteProductAsync(Ulid id);
    }
}
