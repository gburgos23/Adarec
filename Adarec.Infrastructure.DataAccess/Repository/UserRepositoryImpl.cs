using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class UserRepositoryImpl(adarecContext context) : RepositoryImpl<User>(context), IUserRepository
    {
        public async Task<List<TechnicianWorkloadDto>> GetTechnicianWorkloadAsync()
        {
            var result = await (
                from u in context.Users
                where u.Status && u.Roles.Any(r => r.RoleId == 2)
                select new TechnicianWorkloadDto
                {
                    TechnicianId = u.UserId,
                    TechnicianName = u.Name,
                    TechnicianEmail = u.Email,
                    AssignedOrdersCount = u.OrderAssignments.Count()
                }
            ).ToListAsync();

            return result;
        }
    }
}