using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IOrderDetailService
    {

        [OperationContract]
        Task<OrderDetail?> GetOrderDetailByIdAsync(int detailId);

        [OperationContract]
        Task AddOrderDetailAsync(OrderDetail detail);

        [OperationContract]
        Task UpdateOrderDetailAsync(DeviceDetailDto detail);

        [OperationContract]
        Task DeleteOrderDetailAsync(int detailId);

        [OperationContract]
        Task<List<PendingOrderFullDetailDto>> GetAllPendingOrdersWithDetailsAsync();
    }
}