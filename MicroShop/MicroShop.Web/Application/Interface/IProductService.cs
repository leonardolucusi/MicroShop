using MicroShop.Web.Domain.DTOs.ProductDTOs;

namespace MicroShop.Web.Application.Interface
{
    public interface IProductService
    {
        public Task<ProductDTO> GetProductById(string id);
        public Task<IEnumerable<ProductDTO>> GetAllProductsByCartProductsIds(IEnumerable<string> productsIds);
        public Task<ProductDTO> CreateProduct(ProductDTO productDto);
        public Task<ProductDTO> UpdateProduct(ProductDTO productDTO);
        public Task<bool> DeleteProductAsync(string id);
    }
}
