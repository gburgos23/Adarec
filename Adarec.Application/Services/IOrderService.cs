
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        [OperationContract]
        Task<Order?> GetOrderByIdAsync(int orderId);

        [OperationContract]
        Task AddOrderAsync(Order order);

        [OperationContract]
        Task UpdateOrderAsync(Order order);

        [OperationContract]
        Task DeleteOrderAsync(int orderId);
    }
}
