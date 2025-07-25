using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<List<TechnicianDto>> GetAllUsersAsync();

        [OperationContract]
        Task AddUserAsync(TechnicianDto user);

        [OperationContract]
        Task UpdateUserAsync(TechnicianDto user);

        [OperationContract]
        Task<List<TechnicianWorkloadDto>> GetTechnicianWorkloadAsync();
    }
}