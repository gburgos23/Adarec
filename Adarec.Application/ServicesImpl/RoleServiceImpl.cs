using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class RoleServiceImpl(adarecContext context) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = new RoleRepositoryImpl(context);

        public async Task<List<RolDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();

            return roles.Select(role => new RolDto
            {
                RolId = role.RoleId,
                Name = role.Name,
                Status = role.Status
            }).ToList();
        }
    }
}