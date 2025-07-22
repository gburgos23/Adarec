using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IModelRepository : IRepository<Model>
    {
        Task<List<Model>> GetActiveModelsAsync();
    }
}