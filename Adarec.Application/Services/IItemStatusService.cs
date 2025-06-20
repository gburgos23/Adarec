using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IItemStatusService
    {
        [OperationContract]
        Task<IEnumerable<ItemStatus>> GetAllItemStatusesAsync();

        [OperationContract]
        Task<ItemStatus?> GetItemStatusByIdAsync(int itemStatusId);

        [OperationContract]
        Task AddItemStatusAsync(ItemStatus itemStatus);

        [OperationContract]
        Task UpdateItemStatusAsync(ItemStatus itemStatus);

        [OperationContract]
        Task DeleteItemStatusAsync(int itemStatusId);
    }
}