using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using Application.Services.Interfaces;
using Domain;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class CategoryService : GenericService<Category, CategoryDto>, ICategoryService
    {
        public CategoryService(IGenericRepository<Category> repository, IMappingService mapper) : base(repository, mapper)
        {
        }

    }
}
