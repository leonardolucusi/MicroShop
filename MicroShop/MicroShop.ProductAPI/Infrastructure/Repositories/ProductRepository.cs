using MicroShop.ProductAPI.Domain.Entities;
using MicroShop.ProductAPI.Domain.Repositories;
using MicroShop.ProductAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace MicroShop.ProductAPI.Infrastructure.Repositories
{
    public class ProductRepository(Context context) : IProductRepository
    {
        private readonly Context _context = context;
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetByIdAsync(Ulid id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Ulid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}