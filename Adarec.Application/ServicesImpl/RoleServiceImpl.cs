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
            return await _roleRepository.GetAllAsync();
        }

        public async Task AddRoleAsync(RolDto role)
        {
            var entity = new Role
            {
                Name = role.Name,
                Status = role.Status
            };
            await _roleRepository.AddAsync(entity);
        }

        public async Task UpdateRoleAsync(RolDto role)
        {
            var entity = new Role
            {
                RoleId = role.RolId,
                Name = role.Name,
                Status = role.Status
            };
            await _roleRepository.UpdateAsync(entity);
        }
    }
}