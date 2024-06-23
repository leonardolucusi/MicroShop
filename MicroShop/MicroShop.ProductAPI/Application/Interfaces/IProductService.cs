﻿using MicroShop.ProductAPI.Domain.DTOs;

namespace MicroShop.ProductAPI.Application.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetProductsAsync();
        public Task<ProductDTO> GetProductByIdAsync(Ulid id);
        public Task<ProductDTO> CreateProductAsync(ProductDTO productDto);
        public Task UpdateProductAsync(ProductDTO productDto);
        public Task DeleteProductAsync(Ulid id);
    }
}