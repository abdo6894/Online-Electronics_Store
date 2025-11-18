using DAL.Data.Context;
using Domain;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Implementation
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context, ILogger<GenericRepository<OrderItem>> log)
            : base(context, log)
        {
        }

       
    }
}