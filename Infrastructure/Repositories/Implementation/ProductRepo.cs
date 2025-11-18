using DAL.Data.Context;
using Domain;
using Infrastructure.Exceptions;
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
        private readonly ILogger _log;
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context, ILogger<GenericRepository<Product>> log) : base(context, log)
        {
            _context = context;
            _log = log;
        }

      public  async Task<List<Product>> GetAllWithCategoryAsync()
      {
            try
            {
                return await _context.Products
                              .Include(p => p.Category)
                              .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting all for entity of type {typeof(Product).Name}", _log);

            }
        }
        public async Task<Product> GetByIdWithCategoryAsync(Guid id)
        {
            try
            {
                return await _context.Products
                                     .Include(p => p.Category)
                                     .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting entity of type {typeof(Product).Name} with Id {id}", _log);
            }
        }

    }
}
