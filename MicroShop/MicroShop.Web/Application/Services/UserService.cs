using AutoMapper;
using MicroShop.Web.Domain.DTOs;
using MicroShop.Web.Domain.Entities;
using MicroShop.Web.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroShop.Web.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<string> RegisterUserAsync(UserRegisterDTO registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            await _userRepository.AddUserAsync(user);
            return "Ok User Created";
        }
        public async Task<string> AuthenticateAsync(UserLoginDTO loginDto)
        {
            var user = await _userRepository.FindUserByUsernameAsync(loginDto.Username);
            if (user == null || user.Password != loginDto.Password)
            {
                return null;
            }
            var token = GenerateJwtToken(user);
            return token;
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddSeconds(10),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}