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
        Task AddOrderDetailAsync(DeviceDetailDto detail);

        [OperationContract]
        Task UpdateOrderDetailAsync(DeviceDetailDto detail);

        [OperationContract]
        Task DeleteOrderDetailAsync(int detailId);

    }
}