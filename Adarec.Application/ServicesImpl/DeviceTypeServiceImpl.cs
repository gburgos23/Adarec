using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class DeviceTypeServiceImpl(adarecContext context) : IDeviceTypeService
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository = new DeviceTypeRepositoryImpl(context);

        public async Task AddDeviceTypeAsync(DeviceTypeDto data)
        {
            var deviceType = new DeviceType
            {
                Name = data.Name,
                Status = data.Status
            };

            await _deviceTypeRepository.AddAsync(deviceType);
        }

        public async Task UpdateDeviceTypeAsync(DeviceTypeDto data)
        {
            var deviceType = new DeviceType
            {
                DeviceTypeId = data.DeviceTypeId,
                Name = data.Name,
                Status = data.Status
            };
            await _deviceTypeRepository.UpdateAsync(deviceType);
        }

        public async Task<List<DeviceTypeDto>> GetActiveDeviceTypesAsync()
        {
            var deviceTypes = await _deviceTypeRepository.GetActiveDeviceTypesAsync();

            return deviceTypes.Select(dt => new DeviceTypeDto
            {
                DeviceTypeId = dt.DeviceTypeId,
                Name = dt.Name,
                Status = dt.Status
            }).ToList();
        }
    }
}