using AutoMapper;
using BL.Mapping;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.Generic
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


        public bool Add(TDto dto)
        {
            var entity = _mapper.Map<TDto, T>(dto);

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            return _repository.Add(entity);
        }


        public List<TDto> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.MapList<T, TDto>(entities);
        }

        public TDto GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<T, TDto>(entity);
        }

        public bool Update(TDto dto)
        {
            var entity = _mapper.Map<TDto, T>(dto);
            return _repository.Update(entity);
        }
    }
}
