using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IItemStatusService
    {
        [OperationContract]
        Task<List<RolDto>> GetAllItemStatusesAsync();
    }
}