using BL.Dtos.AppUserDtos;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResultDto> RegisterAsync(RegisterDto dto);
        Task<AuthResultDto> LoginAsync(LoginDto dto);
    }

    
}
