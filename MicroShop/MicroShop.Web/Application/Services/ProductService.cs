using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.ProductDTOs;

namespace MicroShop.Web.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/products";

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductAPI");
        }
        public async Task<ProductDTO> GetProductById(string id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.Content.ReadFromJsonAsync<ProductDTO>();
        }
        public async Task<ProductDTO> CreateProduct(ProductDTO productDto)
        {
            var response = await _httpClient.PostAsJsonAsync(($"api/products"), productDto);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductDTO>();
            }
            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{productDTO.Id}", productDTO);
            if (response.IsSuccessStatusCode)
            {
                return productDTO;
            }
            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<bool> DeleteProductAsync(string id) 
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
                return true;
            else throw new Exception("Something went wrong when calling API");
        }
    }
}
