
using DAL.Data.Context;
using Domain;
using Infrastructure.Exceptions;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _log;
        public GenericRepository(AppDbContext context, ILogger<GenericRepository<T>> log)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _log = log;
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                entity.CreatedDate = DateTime.UtcNow;
                 _dbSet.Add(entity);
               await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Adding for entity of type {typeof(T).Name}", _log);

            }
        }

            public async Task<bool> Delete(Guid id)
            {
                try
                {
                    // Load entity with all children (Cascade will work correctly)
                    var entity = await _dbSet
                        .AsQueryable()
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (entity == null)
                        return false;

                    _context.Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DataAccessException(ex, $"Error deleting entity of type {typeof(T).Name}", _log);
                }
            }



        public async Task<List<T>> GetAll()
        {
            try
            {
                return  await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting all for entity of type {typeof(T).Name}", _log);

            }
        }


        public async Task<T> GetById(Guid id)
        {
            try
            {
               return  await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting by Id for entity of type {typeof(T).Name}", _log);
            }
        }

        public async Task<bool> Update(T entity)
        {
 
            try
            {
                var dbData = GetById(entity.Id);
                _context.Entry(entity).State = EntityState.Modified;
              await  _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Updating for entity of type {typeof(T).Name}", _log);
            }
        }

    }
}
