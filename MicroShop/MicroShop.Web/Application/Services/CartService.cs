using MicroShop.Web.Application.Interface;
using MicroShop.Web.Domain.DTOs.CartDTOs;
using System.Reflection.Metadata.Ecma335;

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
        public async Task<CartItemDTO> UpdateOneProductInCartItemByUserIdProductId(UpdateCartItemDTO updateCartItemDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BasePath}", updateCartItemDTO);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CartItemDTO>();
                return result;
            }
            return new CartItemDTO { };
        }
        public async Task<CartItemDTO> GetOneProductfromCartItemByUserIdProductId(int userId, string productId)
        {
            var response = await _httpClient.GetAsync($"api/v1/carts/{userId}/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CartItemDTO>();
                return result;
            }
            return new CartItemDTO { };
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
        public async Task<IEnumerable<CartItemDTO>> GetAllCartItemsInUserId(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BasePath}/{userId}/cartItems");

                if (response.IsSuccessStatusCode)
                {
                    var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemDTO>>();
                    return cartItems ?? new List<CartItemDTO>();
                }
                else
                {
                    Console.WriteLine($"Failed to fetch cart items for user ID {userId}. Status Code: {response.StatusCode}");
                    return new List<CartItemDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching cart items for user ID {userId}: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteAllUserCartItemsByUserId(int userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BasePath}/{userId}");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteOneCartItemInUser(int userId, string productId)
        {
            try
            {
                await _httpClient.DeleteAsync($"{BasePath}/{userId}/{productId}");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
