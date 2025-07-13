using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IEmailTemplateService
    {
        [OperationContract]
        Task<IEnumerable<EmailTemplate>> GetAllEmailTemplatesAsync();

        [OperationContract]
        Task<EmailTemplate?> GetEmailTemplateByIdAsync(int templateId);

        [OperationContract]
        Task AddEmailTemplateAsync(EmailTemplate template);

        [OperationContract]
        Task UpdateEmailTemplateAsync(EmailTemplate template);

        [OperationContract]
        Task DeleteEmailTemplateAsync(int templateId);
    }
}