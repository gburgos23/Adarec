using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IEmailConfigService
    {
        [OperationContract]
        Task<IEnumerable<EmailConfig>> GetAllEmailConfigsAsync();

        [OperationContract]
        Task<EmailConfig?> GetEmailConfigByIdAsync(int configId);

        [OperationContract]
        Task AddEmailConfigAsync(EmailConfig config);

        [OperationContract]
        Task UpdateEmailConfigAsync(EmailConfig config);

        [OperationContract]
        Task DeleteEmailConfigAsync(int configId);
    }
}