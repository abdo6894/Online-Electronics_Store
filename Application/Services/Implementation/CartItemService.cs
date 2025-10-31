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
        public CartItemService(IGenericRepository<CartItem> repository, IMappingService mapper) : base(repository, mapper)
        {
        }

    }

}
