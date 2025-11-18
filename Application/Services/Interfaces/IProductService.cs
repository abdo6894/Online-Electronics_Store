using Application.Dtos;
using Application.Services.Interfaces.Generic;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductService : IGenericService<Product,ProductDto>
    {
        Task<List<ProductDto>> GetProductsWithCategory();
        Task<ProductDto> GetProductWithCategoryById(Guid id);
        Task<bool> AddProduct(ProductDto entity);

    }
}
