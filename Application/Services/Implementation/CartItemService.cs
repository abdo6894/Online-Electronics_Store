using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using Application.Services.Interfaces;
using Domain;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class CartItemService : GenericService<CartItem, CartItemDto>, ICartItemService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMappingService _mapper;

        public CartItemService(ICartRepository cartRepository, IMappingService mapper)
            : base(cartRepository, mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddToCart(AddToCartDto dto,Guid UserId)
        {
            var entity = new CartItem
            {
                Id = Guid.NewGuid(),
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UserId = UserId
            };
            return await _cartRepository.Add(entity);

        }

        public async Task<List<CartItemDto>> GetUserCart(Guid userId)
        {
            var items = await _cartRepository.GetUserCartAsync(userId);

            return items.Select(i => new CartItemDto
            {
                Id = i.Id,
                Quantity = i.Quantity,
                ProductId = i.ProductId,
                ProductName = i.Product!.Name!,
                CategoryName = i.Product.Category!.Name!,
                UserId = i.UserId
            }).ToList();
        }


    }

}
