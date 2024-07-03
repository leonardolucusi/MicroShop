using MicroShop.CartAPI.Domain.DTOs;
using MicroShop.CartAPI.Domain.Entities;

namespace MicroShop.CartAPI.Domain.Repositories
{
    public interface ICartRepository
    {
        public Task<IEnumerable<CartItem>> GetAllCartItemsByUserId(int userId);
        public Task<CartItem> GetCartItemByIdAndUserId(int userId, string productId);
        public Task AddCartItem(CartItem cartItem);
        public Task<bool> RemoveCartItemProduct(int userId, string productId);
        public Task<bool> CheckIfUserHasCartItemProduct(int userId, string productId);
        public Task<CartItem> UpdateCartItemQuantity(UpdateProductQuantityInCartItemDTO updateProductQuantityInCartItemDTO);
        public Task<bool> DeleteAllCartItemsByUserId(int userId);
    }
}
