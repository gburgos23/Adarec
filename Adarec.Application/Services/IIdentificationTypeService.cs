using Adarec.Application.DTO.DTOs;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IIdentificationTypeService
    {
        [OperationContract]
        Task<List<RolDto>> GetAllIdentificationTypesAsync();
    }
}