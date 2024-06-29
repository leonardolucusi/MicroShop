using MicroShop.CartAPI.Domain.DTOs;
using MicroShop.CartAPI.Domain.Entities;

namespace MicroShop.CartAPI.Application.Interfaces
{
    public interface ICartService
    {
        public Task<Cart> AddOrRemoveProductToCartAsync(int userId, string productId);
        public Task<bool> UpdateQuantityInCartItemProduct(UpdateProductQuantityInCartItemDTO updateProductQuantityInCartItemDTO);
        public Task<IEnumerable<CartItem>> GetAllCartItems(int cartId);
    }
}
