using MicroShop.CartAPI.Domain.DTOs;
using MicroShop.CartAPI.Domain.Entities;
using MicroShop.CartAPI.Domain.Repositories;
using MicroShop.CartAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace MicroShop.CartAPI.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _context;
        public CartRepository(Context context)
        {
            _context = context;
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<CartItem> AddCartItemToCart(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> CheckIfCartHasCartItemProduct(int cartId, string productId)
        {
            return await _context.CartItems.AnyAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<Cart> CheckIfUserHasCart(int userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public Task<Cart> FindCartByUserId(int userId)
        {
            return _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId); ;
        }

        public async Task<bool> RemoveCartItemProduct(int cartId, string productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartItem> UpdateQuantity(UpdateProductQuantityInCartItemDTO updateDto)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == updateDto.CartId && ci.ProductId == updateDto.ProductId);
            if (cartItem is null) return null;
            cartItem.Quantity = updateDto.Quantity;
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
    }
}