using Application.Dtos;
using Application.Services.Interfaces.Generic;
using Domain;

namespace Application.Services.Interfaces
{
    public interface ICartItemService : IGenericService<CartItem, CartItemDto>
    {
        Task<List<CartItemDto>> GetUserCart(Guid userId);
        Task<bool> AddToCart(AddToCartDto dto, Guid userId);
    }
}
