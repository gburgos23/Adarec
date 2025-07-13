using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class RoleRepositoryImpl(adarecContext context) : RepositoryImpl<Role>(context), IRoleRepository
    {
        public async Task<List<RolDto>> GetAllAsync()
        {
            try
            {
                return await context.Roles
                    .Select(role => new RolDto
                    {
                        RolId = role.RoleId,
                        Name = role.Name,
                        Status = role.Status
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving roles: {ex.Message}");
            }
        }
    }
}