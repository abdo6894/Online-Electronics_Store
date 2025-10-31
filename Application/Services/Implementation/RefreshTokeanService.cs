using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using Application.Services.Interfaces;
using Domain;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementation
{
    public class RefreshTokeanService : GenericService<RefreshToken, RefreshTokenDto>, IRefreshTokeanService
    {
        public RefreshTokeanService(IGenericRepository<RefreshToken> repository, IMappingService mapper) : base(repository, mapper)
        {
        }

    }

}
