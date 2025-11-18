using DAL.Data.Context;
using Domain;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Implementation
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;


        public OrderRepository(AppDbContext context, ILogger<GenericRepository<Order>> log) : base(context, log)
        {
            _context = context;
            _logger = log;

        }


        public async Task<List<Order>> GetUserOrdersAsync(Guid userId)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                            .ThenInclude(p => p.Category)
                    .OrderByDescending(o => o.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders for user {UserId}", userId);
                throw;
            }

        }


        public async Task<List<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                            .ThenInclude(p => p.Category)
                    .OrderByDescending(o => o.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all orders");
                throw;
            }
        }
    }
}