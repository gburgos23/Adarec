using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Adarec.Application.ServicesImpl
{
    public class UserServiceImpl(adarecContext context) : IUserService
    {
        private readonly IUserRepository _userRepository = new UserRepositoryImpl(context);

        public async Task<List<TechnicianDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(u => new TechnicianDto
            {
                TechnicianId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                IdRol = u.Roles!.Select(r => r.RoleId).ToList()
            }).Where(u => u.Status)
            .ToList();
        }

        public async Task<TechnicianDto?> GetUserByMail(string mail)
        {
            var users = await _userRepository.GetAllUsersAsync();

            return (users.Select(u => new TechnicianDto
            {
                TechnicianId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                IdRol = [.. u.Roles!.Select(r => r.RoleId)]
            }).Where(u => u.Email.Equals(mail, StringComparison.OrdinalIgnoreCase) && u.Status))
            .FirstOrDefault();
        }

        public async Task AddUserAsync(TechnicianDto user)
        {
            var userEntity = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Status = user.Status
            };

            // Asignar roles
            if (user.IdRol != null && user.IdRol.Count > 0)
            {
                foreach (var roleId in user.IdRol)
                {
                    var role = await context.Roles.FindAsync(roleId);
                    if (role != null)
                    {
                        userEntity.Roles.Add(role);
                    }
                }
            }

            await _userRepository.AddAsync(userEntity);
        }

        public async Task DeleteUserAsync(int customerId)
        {
            try
            {
                var customer = await _userRepository.GetByIdAsync(customerId);
                if (customer is not null)
                {
                    customer.Status = false;
                    await _userRepository.UpdateAsync(customer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateUserAsync(TechnicianDto user)
        {
            var userEntity = await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserId == user.TechnicianId) ?? throw new Exception("Usuario no encontrado");

            userEntity.Name = user.Name;
            userEntity.Email = user.Email;
            userEntity.Password = user.Password;
            userEntity.Status = user.Status;

            // Limpiar roles actuales
            userEntity.Roles.Clear();

            // Asignar nuevos roles sin duplicados
            if (user.IdRol != null && user.IdRol.Count > 0)
            {
                foreach (var roleId in user.IdRol.Distinct())
                {
                    var role = await context.Roles.FindAsync(roleId);
                    if (role != null)
                    {
                        userEntity.Roles.Add(role);
                    }
                }
            }

            await _userRepository.UpdateAsync(userEntity);
        }

        public async Task<List<TechnicianWorkloadDto>> GetTechnicianWorkloadAsync()
        {
            var technicians = await _userRepository.GetTechniciansAsync();

            return technicians.Select(u => new TechnicianWorkloadDto
            {
                TechnicianId = u.UserId,
                TechnicianName = u.Name,
                AssignedOrdersCount = u.OrderAssignments?.Count() ?? 0
            }).ToList();
        }
    }
}