using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class UserRepositoryImpl(adarecContext context) : RepositoryImpl<User>(context), IUserRepository
    {
        private readonly adarecContext _context = context;

        public async Task<List<User>> GetTechniciansAsync()
        {
            return await _context.Users
                .Include(u => u.OrderAssignments)
                .Include(u => u.Roles)
                .Where(u => u.Status && u.Roles.Any(r => r.RoleId == 2))
                .ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Status && u.Roles.Any())
                .ToListAsync();
        }
    }
}