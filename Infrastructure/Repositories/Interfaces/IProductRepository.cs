    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Infrastructure.Repositories.Interfaces
    {
        public interface IProductRepository : IGenericRepository<Product>
        {
            Task<List<Product>> GetAllWithCategoryAsync();
            public Task<Product> GetByIdWithCategoryAsync(Guid id);
        }
    }
