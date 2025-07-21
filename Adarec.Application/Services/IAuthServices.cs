using Adarec.Application.DTO.DTOs;

namespace Adarec.Application.Services
{
    public interface IAuthServices
    {
        Task<ApiResponseDto> ValidateLogin(LoginDto login);
    }
}