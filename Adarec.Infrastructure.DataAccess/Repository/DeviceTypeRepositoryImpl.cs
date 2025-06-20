using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class DeviceTypeRepositoryImpl(adarecContext context) : RepositoryImpl<DeviceType>(context), IDeviceTypeRepository
    {
    }
}