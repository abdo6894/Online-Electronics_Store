using AutoMapper;
using BL.Dtos.CartItemDtos;
using BL.Dtos.CategoryDtos;
using BL.Dtos.OrderDtos;
using BL.Dtos.OrderItemDtos;
using BL.Dtos.ProductDtos;
using BL.Dtos.RefreshTokenDtos;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mapping
{
    public  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartItem, CartItemCreateDto>().ReverseMap();
            CreateMap<CartItem, CartItemDetailsDto>().ReverseMap();
            CreateMap<CartItem, CartItemUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryDetailsDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemCreateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDetailsDto>().ReverseMap();

            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderDetailsDto>().ReverseMap();
            CreateMap<Order, OrderUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDetailsDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap();

            CreateMap<RefreshToken, RefreshTokenCreateDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenRequestDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenResponseDto>().ReverseMap();
        
        }
    }
}
