using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.ProductDTOs;
using System.Text.Json;

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
        public async Task<IEnumerable<ProductDTO>> GetAllProductsByCartProductsIds(IEnumerable<string> productIds)
        {
            try
            {
                var queryString = string.Join("&", productIds.Select(id => $"productIds={Uri.EscapeDataString(id)}"));

                var apiUrl = $"https://localhost:7037/api/products/cart?{queryString}";

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productsDto = JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (productsDto == null)
                    {
                        throw new Exception("Failed to deserialize products from response.");
                    }

                    return productsDto;
                }
                else
                {
                    throw new Exception($"Failed to retrieve products. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve products by cart item IDs.", ex);
            }
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
