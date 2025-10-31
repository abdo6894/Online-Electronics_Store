using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.Generic
{
   public  interface IGenericService <T,TDto>
        where T : BaseEntity
       where TDto : class
    {
     Task<TDto> GetById(Guid id);
     Task< List<TDto>> GetAll();
     Task<bool> Add(TDto entity);
     Task<bool> Update(TDto entity);
     Task<bool> Delete(Guid id);


    }
}
