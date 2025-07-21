using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<int> AddOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);

        Task<List<Order>> ListPendingOrdersByTechnicianAsync();

        Task<Order?> GetOrderDetailByIdAsync(int orderId);

        Task<List<Order>> GetAllOrders();

        Task<List<Order>> GetTicketCountByStatusAsync(int year, int month, int? technicianId = null);

        Task<Order?> GetOrderStatusHistoryAsync(int orderId);

        Task<List<Solution>> GetSolutionsByOrderAsync(int orderId);
    }
}
