using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<int> AddOrderAsync(OrderDto order);

        Task UpdateOrderAsync(OrderDto order);

        Task<List<TechnicianPendingOrdersDto>> ListPendingOrdersByTechnicianAsync();

        Task<OrderFullDetailDto?> GetOrderDetailByIdAsync(int orderId);

        Task<List<OrderFullDetailDto>> GetOrderDetailByCustomerDocumentAsync(string identificationNumber);

        Task<List<TicketCountByStatusDto>> GetTicketCountByStatusAsync(int year, int month, int? technicianId = null);

        Task<List<SolutionDetailDto>> GetSolutionsByOrderAsync(int orderId);
    }
}
