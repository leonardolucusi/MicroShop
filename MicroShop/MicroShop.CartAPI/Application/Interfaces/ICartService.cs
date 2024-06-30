using MicroShop.CartAPI.Domain.DTOs;

namespace MicroShop.CartAPI.Application.Interfaces
{
    public interface ICartService
    {
        public Task<bool> AddOrRemoveProductToCartAsync(int userId, string productId);
        public Task<bool> UpdateQuantityInCartItemProduct(UpdateProductQuantityInCartItemDTO updateProductQuantityInCartItemDTO);
        public Task<IEnumerable<CartItemDTO>> GetAllCartItems(int cartId);
    }
}
