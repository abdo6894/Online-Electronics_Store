using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using Application.Services.Interfaces;
using Domain;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class OrderService : GenericService<Order, OrderDto>, IOrderService
    {
        public OrderService(IGenericRepository<Order> repository, IMappingService mapper) : base(repository, mapper)
        {
        }

    }
}
