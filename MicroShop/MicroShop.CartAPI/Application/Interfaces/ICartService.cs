using MicroShop.CartAPI.Domain.DTOs;

namespace MicroShop.CartAPI.Application.Interfaces
{
    public interface ICartService
    {
        public Task<CartItemDTO> GetOneCartItemByProductIdAndUserId(int userId, string productId);
        public Task<bool> AddOrRemoveCartItemAsync(int userId, string productId);
        public Task<bool> UpdateQuantityInCartItemProduct(UpdateProductQuantityInCartItemDTO updateProductQuantityInCartItemDTO);
        public Task<IEnumerable<CartItemDTO>> GetAllCartItemsByUserId(int userId);
        public Task<bool> DeleteAllCartItemsByUserId(int userId);
    }
}
