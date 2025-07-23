using Adarec.Application.DTO.DTOs;
using Adarec.Infrastructure.MailServices.Models;

namespace Adarec.Infrastructure.MailServices.Services
{
    public interface ISendMailService
    {
        Task SendOrderCompletionMailAsync(OrderDto order);
    }
}