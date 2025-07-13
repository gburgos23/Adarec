using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IRoleService
    {
        [OperationContract]
        Task<List<RolDto>> GetAllRolesAsync();

        [OperationContract]
        Task AddRoleAsync(RolDto role);

        [OperationContract]
        Task UpdateRoleAsync(RolDto role);
    }
}