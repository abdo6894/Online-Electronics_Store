using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Generic;
using Domain;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMappingService _mapper;

        public ProductService(IProductRepository productRepository, IMappingService mapper)
            : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetAllWithCategoryAsync();
            return _mapper.MapList<Product,ProductDto>(products);
        }

        public async Task<ProductDto> GetProductWithCategoryById(Guid id)
        {
            var product = await _productRepository.GetByIdWithCategoryAsync(id);

            return _mapper.Map<Product, ProductDto>(product);
        }
    }

}
