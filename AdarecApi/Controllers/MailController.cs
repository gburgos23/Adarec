using Adarec.Application.DTO.DTOs;
using Adarec.Infrastructure.MailServices.Services;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController(ISendMailService sendMailService) : ControllerBase
    {
        private readonly ISendMailService _sendMailService = sendMailService;
        [Route("send-mail")]
        [HttpPost]
        public async Task<IActionResult> SendOrderCompletionMail([FromBody] OrderDto order)
        {
            try
            {

                await _sendMailService.SendOrderCompletionMailAsync(order);
                return Ok("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
