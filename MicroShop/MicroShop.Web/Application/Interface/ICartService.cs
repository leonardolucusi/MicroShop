using MicroShop.Web.Domain.DTOs.CartDTOs;

namespace MicroShop.Web.Application.Interface
{
    public interface ICartService
    {
        public Task<AddProductToCartDTO> AddProductToCart(AddProductToCartDTO addProductToCartDTO);
    }
}
