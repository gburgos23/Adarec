using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Infrastructure.CrossCuting.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices, IEncriptServices encriptServices) : Controller
    {
        private readonly IAuthServices _service = authServices;
        private readonly IEncriptServices _encriptService = encriptServices;

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await _service.ValidateLogin(login);
            return StatusCode(result.StatusCode, result.Message);
        }

        [Route("encript")]
        [HttpPost]
        public string Encrypt(string text)
        {
            return _encriptService.Encrypt(text);
        }
    }
}
