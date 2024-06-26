using MicroShop.Web.Domain.Entities;
using MicroShop.Web.Domain.Interfaces;
using MicroShop.Web.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Web.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindUserByUsernameAsync(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
            return user;
        }
    }
}
