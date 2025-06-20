using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface ISolutionService
    {
        [OperationContract]
        Task<IEnumerable<Solution>> GetAllSolutionsAsync();

        [OperationContract]
        Task<Solution?> GetSolutionByIdAsync(int solutionId);

        [OperationContract]
        Task AddSolutionAsync(Solution solution);

        [OperationContract]
        Task UpdateSolutionAsync(Solution solution);

        [OperationContract]
        Task DeleteSolutionAsync(int solutionId);
    }
}