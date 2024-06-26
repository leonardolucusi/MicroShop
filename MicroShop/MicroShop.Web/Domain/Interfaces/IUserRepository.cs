using MicroShop.Web.Domain.Entities;
namespace MicroShop.Web.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task UserAddAsync(User user);
        public Task<User> FindUserByUsernameAsync(string userName);
        public Task<User> FindUserByIdAsync(int id);
        public Task<User> UserUpdateAsync(User user);
    }
}
