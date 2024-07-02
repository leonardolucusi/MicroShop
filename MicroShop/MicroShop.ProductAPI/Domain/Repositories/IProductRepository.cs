using MicroShop.ProductAPI.Domain.Entities;
namespace MicroShop.ProductAPI.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IQueryable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Ulid id);
        public Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<string> productIds);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Ulid id);
    }
}
