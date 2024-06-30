using AutoMapper;
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
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrRemoveProductToCartAsync(int userId, string productId)
        {
            var cartExist = await _cartRepository.FindCartByUserId(userId);
            if (cartExist is not null)
            {
                if (await _cartRepository.CheckIfCartHasCartItemProduct(cartExist.Id, productId) is true)
                {
                    await _cartRepository.RemoveCartItemProduct(cartExist.Id, productId);
                    return false;
                };

                await _cartRepository.AddCartItemToCart(new CartItem
                {
                    CartId = cartExist.Id,
                    ProductId = productId,
                    Quantity = 1
                });
                return true;
            }
            var newCart = await _cartRepository.AddCart(new Cart { UserId = userId });
            await _cartRepository.AddCartItemToCart(new CartItem
            {
                CartId = newCart.Id,
                ProductId = productId,
                Quantity = 1
            });
            return true;
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllCartItems(int cartId)
        {
            var cartItemsDto = await _cartRepository.GetAllCartItems(cartId);
            return _mapper.Map<IEnumerable<CartItemDTO>>(cartItemsDto);
        }

        public async Task<bool> UpdateQuantityInCartItemProduct(UpdateProductQuantityInCartItemDTO updateProductQuantityInCartItemDTO)
        {
            if(await _cartRepository.UpdateQuantity(updateProductQuantityInCartItemDTO) is null) { return false; }
            return true;
        }
    }
}