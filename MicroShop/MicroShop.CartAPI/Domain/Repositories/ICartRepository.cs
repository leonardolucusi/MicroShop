using MicroShop.CartAPI.Domain.DTOs;
using MicroShop.CartAPI.Domain.Entities;
namespace MicroShop.CartAPI.Domain.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart> AddCart(Cart cart);
        public Task<Cart> CheckIfUserHasCart(int userId);
        public Task<Cart> FindCartByUserId(int userId);
        public Task<CartItem> AddCartItemToCart(CartItem cartItem);
        public Task<bool> CheckIfCartHasCartItemProduct(int cartId, string productId);
        public Task<bool> RemoveCartItemProduct(int cartId, string productId);
        public Task<CartItem> UpdateQuantity(UpdateProductQuantityInCartItemDTO updateProductQuantityInCartItemDTO);
    }
}