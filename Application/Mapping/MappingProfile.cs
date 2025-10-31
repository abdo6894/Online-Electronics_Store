using Application.Dtos;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartItem, CartItemDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();


            CreateMap<OrderItem, OrderItemDto>().ReverseMap();


            CreateMap<Order, OrderDto>().ReverseMap();


            CreateMap<Product, ProductDto>()
          .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
          .ReverseMap();





            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();

        }
    }
}
