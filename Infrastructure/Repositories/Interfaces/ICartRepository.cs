using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartItem>
    {
        Task<List<CartItem>> GetUserCartAsync(Guid userId);
    }

}

