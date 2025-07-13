using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class DeviceTypeRepositoryImpl(adarecContext context) : RepositoryImpl<DeviceType>(context), IDeviceTypeRepository
    {
        private readonly adarecContext _context = context;

        public async Task<List<DeviceTypeDto>> GetActiveDeviceTypesAsync()
        {
            return await _context.DeviceTypes
                .Where(dt => dt.Status)
                .Select(dt => new DeviceTypeDto
                {
                    DeviceTypeId = dt.DeviceTypeId,
                    Name = dt.Name
                }).ToListAsync();
        }
    }
}