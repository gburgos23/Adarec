using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IRoleService
    {
        [OperationContract]
        Task<IEnumerable<Role>> GetAllRolesAsync();

        [OperationContract]
        Task<Role?> GetRoleByIdAsync(int roleId);

        [OperationContract]
        Task AddRoleAsync(Role role);

        [OperationContract]
        Task UpdateRoleAsync(Role role);

        [OperationContract]
        Task DeleteRoleAsync(int roleId);
    }
}