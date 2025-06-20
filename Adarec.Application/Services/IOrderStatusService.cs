using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IOrderStatusService
    {
        [OperationContract]
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusesAsync();

        [OperationContract]
        Task<OrderStatus?> GetOrderStatusByIdAsync(int orderStatusId);

        [OperationContract]
        Task AddOrderStatusAsync(OrderStatus orderStatus);

        [OperationContract]
        Task UpdateOrderStatusAsync(OrderStatus orderStatus);

        [OperationContract]
        Task DeleteOrderStatusAsync(int orderStatusId);
    }
}