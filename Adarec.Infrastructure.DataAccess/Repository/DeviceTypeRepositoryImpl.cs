using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class DeviceTypeRepositoryImpl(adarecContext context) : RepositoryImpl<DeviceType>(context), IDeviceTypeRepository
    {
        public async Task<List<DeviceTypeDto>> GetActiveDeviceTypesAsync()
        {
            return await context.DeviceTypes
                .Where(dt => dt.Status)
                .Select(dt => new DeviceTypeDto
                {
                    DeviceTypeId = dt.DeviceTypeId,
                    Name = dt.Name
                }).ToListAsync();
        }
    }
}