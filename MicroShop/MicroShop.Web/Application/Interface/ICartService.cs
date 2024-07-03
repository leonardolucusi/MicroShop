using MicroShop.Web.Domain.DTOs.CartDTOs;

namespace MicroShop.Web.Application.Interface
{
    public interface ICartService
    {
        public Task<CartItemDTO> GetOneProductfromCartItemByUserIdProductId(int userId, string productId);
        public Task<CartItemDTO> UpdateOneProductInCartItemByUserIdProductId(UpdateCartItemDTO updateCartItemDTO);
        public Task<bool> AddProductToCart(AddProductToCartDTO addProductToCartDTO);
        public Task<IEnumerable<CartItemDTO>> GetAllCartItemsInUserId(int userId);
    }
}
