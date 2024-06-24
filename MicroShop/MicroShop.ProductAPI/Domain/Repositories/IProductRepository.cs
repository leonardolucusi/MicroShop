using MicroShop.ProductAPI.Domain.Entities;
namespace MicroShop.ProductAPI.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IQueryable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Ulid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Ulid id);
    }
}
