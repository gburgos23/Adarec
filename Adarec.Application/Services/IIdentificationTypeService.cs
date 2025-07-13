using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IIdentificationTypeService
    {
        [OperationContract]
        Task<IEnumerable<IdentificationType>> GetAllIdentificationTypesAsync();

        [OperationContract]
        Task<IdentificationType?> GetIdentificationTypeByIdAsync(int identificationTypeId);

        [OperationContract]
        Task AddIdentificationTypeAsync(IdentificationType identificationType);

        [OperationContract]
        Task UpdateIdentificationTypeAsync(IdentificationType identificationType);

        [OperationContract]
        Task DeleteIdentificationTypeAsync(int identificationTypeId);
    }
}