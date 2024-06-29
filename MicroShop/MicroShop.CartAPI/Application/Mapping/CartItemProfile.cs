using AutoMapper;
using MicroShop.CartAPI.Domain.DTOs;
using MicroShop.CartAPI.Domain.Entities;

namespace MicroShop.CartAPI.Application.Mapping
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
        }
    }
}
