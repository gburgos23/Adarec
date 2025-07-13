using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IDeviceTypeService
    {
        [OperationContract]
        Task AddDeviceTypeAsync(DeviceTypeDto data);

        [OperationContract]
        Task UpdateDeviceTypeAsync(DeviceTypeDto data);
    
        [OperationContract]
        Task<List<DeviceTypeDto>> GetActiveDeviceTypesAsync();
    }
}