using Application.Dtos;
using Application.Services.Interfaces.Generic;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IOrderItemService : IGenericService<OrderItem, OrderItemDto>
    {

    }
}
