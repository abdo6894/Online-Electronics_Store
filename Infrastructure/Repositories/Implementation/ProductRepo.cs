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
    public class  ProductRepo : GenericRepository<Product>, IProductRepository
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context, ILogger<GenericRepository<Product>> log) : base(context, log)
        {
            _context = context;
            _logger = log;
        }

      public  async Task<List<Product>> GetAllWithCategoryAsync()
      {
            return await _context.Products
                          .Include(p => p.Category)
                          .ToListAsync();
      }
        public async Task<Product> GetByIdWithCategoryAsync(Guid id)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
