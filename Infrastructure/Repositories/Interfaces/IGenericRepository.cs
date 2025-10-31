using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository <T> where T : BaseEntity 
    {
     Task<T> GetById(Guid id);
     Task<List<T>> GetAll();
     Task<bool> Add(T entity);
     Task<bool> Update(T entity);
     Task<bool> Delete(Guid Id);



    }
}
