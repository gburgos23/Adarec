﻿
using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        Task<int> AddOrderAsync(OrderDto order);

        [OperationContract]
        Task UpdateOrderAsync(OrderDto order);

        [OperationContract]
        Task<List<TechnicianPendingOrdersDto>> ListPendingOrdersByTechnicianAsync();

        [OperationContract]
        Task<OrderFullDetailDto?> GetOrderDetailByIdAsync(int orderId);

        [OperationContract]
        Task<List<OrderFullDetailDto>> GetOrderDetailByCustomerDocumentAsync(string identificationNumber);

        [OperationContract]
        Task<List<TicketCountByStatusDto>> GetTicketCountByStatusAsync(int year, int month, int? technicianId = null);

        [OperationContract]
        Task<List<SolutionDetailDto>> GetSolutionsByOrderAsync(int orderId);
    }
}
