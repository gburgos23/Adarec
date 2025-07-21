using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<List<Role>> GetAllRolesAsync();
    }
}