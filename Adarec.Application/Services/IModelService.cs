using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IModelService
    {
        [OperationContract]
        Task<IEnumerable<Model>> GetAllModelsAsync();

        [OperationContract]
        Task<Model?> GetModelByIdAsync(int modelId);

        [OperationContract]
        Task AddModelAsync(Model model);

        [OperationContract]
        Task UpdateModelAsync(Model model);

        [OperationContract]
        Task DeleteModelAsync(int modelId);
    }
}