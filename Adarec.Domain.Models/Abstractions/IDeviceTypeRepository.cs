using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IDeviceTypeRepository : IRepository<DeviceType>
    {
        Task<List<DeviceTypeDto>> GetActiveDeviceTypesAsync();
    }
}