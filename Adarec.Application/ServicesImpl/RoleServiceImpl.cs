using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class RoleServiceImpl(adarecContext context) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = new RoleRepositoryImpl(context);

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _roleRepository.GetByIdAsync(roleId);
        }

        public async Task AddRoleAsync(Role role)
        {
            await _roleRepository.AddAsync(role);
        }

        public async Task UpdateRoleAsync(Role role)
        {
            await _roleRepository.UpdateAsync(role);
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            await _roleRepository.DeleteAsync(roleId);
        }
    }
}