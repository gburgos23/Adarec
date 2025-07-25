using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<TechnicianWorkloadDto>> GetTechnicianWorkloadAsync();
        Task<List<TechnicianDto>> GetAllUsersAsync();
    }
}