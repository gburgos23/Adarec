using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Application.ServicesImpl
{
    public class OrderServiceImpl(adarecContext context) : IOrderService
    {
        private readonly adarecContext _context = context;
        private readonly IOrderRepository _orderRepository = new OrderRepositoryImpl(context);

        public async Task<int> AddOrderAsync(OrderDto orderDto)
        {
            return await _orderRepository.AddOrderAsync(orderDto);
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            await _orderRepository.UpdateOrderAsync(orderDto);
        }

        public async Task<List<TechnicianPendingOrdersDto>> ListPendingOrdersByTechnicianAsync()
        {
            return await _orderRepository.ListPendingOrdersByTechnicianAsync();
        }

        public async Task<OrderFullDetailDto?> GetOrderDetailByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderDetailByIdAsync(orderId);
        }

        public async Task<List<OrderFullDetailDto>> GetOrderDetailByCustomerDocumentAsync(string identificationNumber)
        {
            return await _orderRepository.GetOrderDetailByCustomerDocumentAsync(identificationNumber);
        }

        public async Task<List<TicketCountByStatusDto>> GetTicketCountByStatusAsync(int year, int month, int? technicianId = null)
        {
            return await _orderRepository.GetTicketCountByStatusAsync(year, month, technicianId);
        }

        public async Task<List<SolutionDetailDto>> GetSolutionsByOrderAsync(int orderId)
        {
            return await _orderRepository.GetSolutionsByOrderAsync(orderId);
        }
    }
}
