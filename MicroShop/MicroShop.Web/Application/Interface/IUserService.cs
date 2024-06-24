using MicroShop.Web.Domain.DTOs;

namespace MicroShop.Web.Application
{
    public interface IUserService
    {
        public Task<string> RegisterUserAsync(UserRegisterDTO registerDto);
        public Task<string> AuthenticateAsync(UserLoginDTO loginDto);
    }
}
