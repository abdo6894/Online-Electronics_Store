using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using Application.Services.Interfaces;
using Domain;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class OrderItemService : GenericService<OrderItem, OrderItemDto>, IOrderItemService
    {
        public OrderItemService(IGenericRepository<OrderItem> repository, IMappingService mapper) : base(repository, mapper)
        {
        }

    }

}
