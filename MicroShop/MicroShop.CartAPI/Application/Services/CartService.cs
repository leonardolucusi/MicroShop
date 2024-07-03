using AutoMapper;
using MicroShop.CartAPI.Application.Interfaces;
using MicroShop.CartAPI.Domain.DTOs;
using MicroShop.CartAPI.Domain.Entities;
using MicroShop.CartAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.CartAPI.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository, IMapper mapper, ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CartItemDTO> GetOneCartItemByProductIdAndUserId(int userId, string productId)
        {
            try
            {
                return _mapper.Map<CartItemDTO>(await _cartRepository.GetCartItemByIdAndUserId(userId, productId));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> AddOrRemoveCartItemAsync(int userId, string productId)
        {
            try
            {
                var productAlreadyInCart = await _cartRepository.CheckIfUserHasCartItemProduct(userId, productId);
                if (productAlreadyInCart)
                {
                    await _cartRepository.RemoveCartItemProduct(userId, productId);
                    return false;
                }

                await _cartRepository.AddCartItem(new CartItem { UserId = userId, ProductId = productId, Quantity = 1 });
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, $"Erro de banco de dados ao adicionar/remover CartItem: {dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar/remover CartItem: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllCartItemsByUserId(int userId)
        {
            try
            {
                var cartItemsDto = await _cartRepository.GetAllCartItemsByUserId(userId);
                return _mapper.Map<IEnumerable<CartItemDTO>>(cartItemsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter itens do carrinho para o usuário {userId}: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> UpdateQuantityInCartItemProduct(UpdateProductQuantityInCartItemDTO productDto)
        {
            try
            {
                if (productDto.Quantity <= 0) return false;
                if (productDto.Quantity > 1)
                    return await _cartRepository.UpdateCartItemQuantity(productDto) is not null;
                if (productDto.AddOrRemove is true && productDto.Quantity == 1)
                {
                    var cartItem = await _cartRepository.GetCartItemByIdAndUserId(productDto.UserId, productDto.ProductId);
                    if (cartItem != null)
                    {
                        productDto.Quantity = cartItem.Quantity + 1;
                        return await _cartRepository.UpdateCartItemQuantity(productDto) is not null;
                    }
                }
                if (productDto.AddOrRemove is false && productDto.Quantity == 1)
                {
                    var cartItem = await _cartRepository.GetCartItemByIdAndUserId(productDto.UserId, productDto.ProductId);
                    if (cartItem != null)
                    {
                        if (cartItem.Quantity == 1) return false;
                        productDto.Quantity = cartItem.Quantity - 1;
                        return await _cartRepository.UpdateCartItemQuantity(productDto) is not null;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar quantidade do item no carrinho: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteAllCartItemsByUserId(int userId)
        {
            try
            {
                return await _cartRepository.DeleteAllCartItemsByUserId(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar quantidade do item no carrinho: {ex.Message}");
                return false;
            }
        }
    }

}