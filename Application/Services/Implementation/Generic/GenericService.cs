using Application.Mapping;
using Application.Services.Interfaces.Generic;
using AutoMapper;
using Domain;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation.Generic
{
    public class GenericService<T, TDto> : IGenericService<T, TDto>
        where T : BaseEntity
        where TDto : class
    {

        private readonly IGenericRepository<T> _repository;
        private readonly IMappingService _mapper;

        public GenericService(IGenericRepository<T> repository, IMappingService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task< bool> Add(TDto dto)
        {
            var entity =  _mapper.Map<TDto, T>(dto);

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            return await _repository.Add(entity);
        }

        public async Task<bool> Delete(Guid id)
        {

            return await  _repository.Delete(id);
        }

        public async Task< List<TDto>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.MapList<T, TDto>(entities);
        }

        public async Task<TDto> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<T, TDto>(entity);
        }

        public async Task<bool> Update(TDto dto)
        {
            var entity = _mapper.Map<TDto, T>(dto);
            return await _repository.Update(entity);
        }

    
    }
}
