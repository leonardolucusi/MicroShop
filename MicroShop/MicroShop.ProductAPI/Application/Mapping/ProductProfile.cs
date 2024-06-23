using AutoMapper;
using MicroShop.ProductAPI.Domain.DTOs;
using MicroShop.ProductAPI.Domain.Entities;

namespace MicroShop.ProductAPI.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
        }
    }
}
