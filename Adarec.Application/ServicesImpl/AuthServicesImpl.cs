using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Infrastructure.CrossCuting.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adarec.Application.ServicesImpl
{
    public class AuthServicesImpl(IUserService userService, IEncriptServices encriptServices) : IAuthServices
    {
        private readonly IUserService _userService = userService;
        private readonly IEncriptServices _encriptServices = encriptServices;

        public async Task<ApiResponseDto> ValidateLogin(LoginDto login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return new ApiResponseDto
                {
                    StatusCode = 400,
                    Message = "El nombre de usuario y la contraseña no pueden estar vacíos."
                };
            }

            var user = await _userService.GetUserByMail(login.Username);

            if (user == null)
            {
                return new ApiResponseDto
                {
                    StatusCode = 404,
                    Message = "Usuario no encontrado."
                };
            }

            var pass = _encriptServices.Encrypt(login.Password);

            if (login.Username == user.Email && user.Password == pass)
            {
                return new ApiResponseDto
                {
                    StatusCode = 200,
                    Message = JsonSerializer.Serialize(user)
                };
            }

            return new ApiResponseDto
            {
                StatusCode = 401,
                Message = "Nombre de usuario o contraseña inválidos."
            };
        }
    }
}
