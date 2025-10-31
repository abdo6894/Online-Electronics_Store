using Application.Dtos.AppUserDtos;

namespace Application.Services.Interfaces.Generic
{
    public interface IUserService
    {
        Task<AuthResultDto> RegisterAsync(RegisterDto dto);
        Task<AuthResultDto> LoginAsync(LoginDto dto);
    }

    
}
