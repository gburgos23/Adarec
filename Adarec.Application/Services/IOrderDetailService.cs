using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IOrderDetailService
    {
        [OperationContract]
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();

        [OperationContract]
        Task<OrderDetail?> GetOrderDetailByIdAsync(int detailId);

        [OperationContract]
        Task AddOrderDetailAsync(OrderDetail detail);

        [OperationContract]
        Task UpdateOrderDetailAsync(OrderDetail detail);

        [OperationContract]
        Task DeleteOrderDetailAsync(int detailId);

        [OperationContract]
        Task<List<PendingOrderFullDetailDto>> GetAllPendingOrdersWithDetailsAsync();
    }
}