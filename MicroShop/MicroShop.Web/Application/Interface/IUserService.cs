using MicroShop.Web.Domain.DTOs.UserDTOs;
using MicroShop.Web.Domain.Entities;

namespace MicroShop.Web.Application
{
    public interface IUserService
    {
        public Task<string> RegisterUserAsync(UserRegisterDTO registerDto);
        public Task<string> AuthenticateAsync(UserLoginDTO loginDto);
        public Task<User> GetUserById(int id);
    }
}
