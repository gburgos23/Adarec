using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Application.ServicesImpl
{
    public class UserServiceImpl(adarecContext context) : IUserService
    {
        private readonly IUserRepository _userRepository = new UserRepositoryImpl(context);

        public async Task<List<TechnicianDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
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

            // Asignar rol (uno solo)
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
            return await _userRepository.GetTechnicianWorkloadAsync();
        }
    }
}