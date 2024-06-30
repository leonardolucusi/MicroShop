using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.CartDTOs;

namespace MicroShop.Web.Application.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/carts";

        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CartAPI");
        }
        public async Task<bool> AddProductToCart(AddProductToCartDTO addProductToCartDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/v1/carts", addProductToCartDTO);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return bool.Parse(result);
            }

            return false;
        }
    }
}
