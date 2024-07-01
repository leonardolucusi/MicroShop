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
        public async Task<IEnumerable<CartItem>> GetAllCartItems(int userId)
        {
            return await _context.CartItems.Where(ci => ci.UserId == userId).ToListAsync();
        }
        public async Task AddCartItem(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CheckIfUserHasCartItemProduct(int userId, string productId)
        {
            return await _context.CartItems.AnyAsync(ci => ci.UserId == userId && ci.ProductId == productId);
        }
        public async Task<bool> RemoveCartItemProduct(int userId, string productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<CartItem> UpdateCartItemQuantity(UpdateProductQuantityInCartItemDTO updateDto)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.UserId == updateDto.UserId && ci.ProductId == updateDto.ProductId);
            if (cartItem is null) return null;
            cartItem.Quantity = updateDto.Quantity;
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
    }
}