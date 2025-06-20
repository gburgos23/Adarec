using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class DeviceTypeServiceImpl(adarecContext context) : IDeviceTypeService
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository = new DeviceTypeRepositoryImpl(context);

        public async Task<IEnumerable<DeviceType>> GetAllDeviceTypesAsync()
        {
            return await _deviceTypeRepository.GetAllAsync();
        }

        public async Task<DeviceType?> GetDeviceTypeByIdAsync(int deviceTypeId)
        {
            return await _deviceTypeRepository.GetByIdAsync(deviceTypeId);
        }

        public async Task AddDeviceTypeAsync(DeviceType deviceType)
        {
            await _deviceTypeRepository.AddAsync(deviceType);
        }

        public async Task UpdateDeviceTypeAsync(DeviceType deviceType)
        {
            await _deviceTypeRepository.UpdateAsync(deviceType);
        }

        public async Task DeleteDeviceTypeAsync(int deviceTypeId)
        {
            await _deviceTypeRepository.DeleteAsync(deviceTypeId);
        }
    }
}