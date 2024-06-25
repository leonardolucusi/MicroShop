using AutoMapper;
using MicroShop.Web.Domain.DTOs.UserDTOs;
using MicroShop.Web.Domain.Entities;

namespace MicroShop.Web.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRegisterDTO>().ReverseMap();
        }
    }
}
