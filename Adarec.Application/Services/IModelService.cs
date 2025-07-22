using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IModelService
    {
        [OperationContract]
        Task<List<ModelDto>> GetActiveModelsAsync();
    }
}