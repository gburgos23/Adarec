using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IDeviceTypeService
    {
        [OperationContract]
        Task<IEnumerable<DeviceType>> GetAllDeviceTypesAsync();

        [OperationContract]
        Task<DeviceType?> GetDeviceTypeByIdAsync(int deviceTypeId);

        [OperationContract]
        Task AddDeviceTypeAsync(DeviceType deviceType);

        [OperationContract]
        Task UpdateDeviceTypeAsync(DeviceType deviceType);

        [OperationContract]
        Task DeleteDeviceTypeAsync(int deviceTypeId);
    }
}