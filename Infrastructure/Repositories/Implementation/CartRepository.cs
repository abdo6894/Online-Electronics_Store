using DAL.Data.Context;
using Domain;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation
{
    public class CartRepository : GenericRepository<CartItem>, ICartRepository
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context, ILogger<GenericRepository<CartItem>> log) : base(context, log)
        {
            _context = context;
            _logger = log;
        }

        public async Task<List<CartItem>> GetUserCartAsync(Guid userId)
        {
            try
            {
                return await _context.CartItems
                    .Include(c => c.Product)
                    .ThenInclude(p => p.Category)
                    .Where(c => c.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving cart items for user {userId}: {ex.Message}");
            }
        }
    }
}