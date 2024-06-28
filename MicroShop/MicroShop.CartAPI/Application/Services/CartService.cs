using MicroShop.CartAPI.Application.Interfaces;
using MicroShop.CartAPI.Domain.DTOs;
using MicroShop.CartAPI.Domain.Entities;
using MicroShop.CartAPI.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace MicroShop.CartAPI.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        [Authorize]
        public async Task<Cart> AddOrRemoveProductToCartAsync(int userId, string productId)
        {
            var cartExist = await _cartRepository.FindCartByUserId(userId);
            if (cartExist is not null)
            {
                if (await _cartRepository.CheckIfCartHasCartItemProduct(cartExist.Id, productId) == true)
                {
                    await _cartRepository.RemoveCartItemProduct(cartExist.Id, productId);
                    return cartExist;
                };

                await _cartRepository.AddCartItemToCart(new CartItem
                {
                    CartId = cartExist.Id,
                    ProductId = productId,
                    Quantity = 1
                });
                return cartExist;
            }
            var newCart = await _cartRepository.AddCart(new Cart { UserId = userId });
            await _cartRepository.AddCartItemToCart(new CartItem
            {
                CartId = newCart.Id,
                ProductId = productId,
                Quantity = 1
            });
            return newCart;
        }

        public async Task<bool> UpdateQuantityInCartItemProduct(UpdateProductQuantityInCartItemDTO updateProductQuantityInCartItemDTO)
        {
            if(await _cartRepository.UpdateQuantity(updateProductQuantityInCartItemDTO) is null) { return false; }
            return true;
        }
    }
}