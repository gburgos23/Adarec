using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class OrderStatusServiceImpl(adarecContext context) : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository = new OrderStatusRepositoryImpl(context);

        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusesAsync()
        {
            return await _orderStatusRepository.GetAllAsync();
        }

        public async Task<OrderStatus?> GetOrderStatusByIdAsync(int orderStatusId)
        {
            return await _orderStatusRepository.GetByIdAsync(orderStatusId);
        }

        public async Task AddOrderStatusAsync(OrderStatus orderStatus)
        {
            await _orderStatusRepository.AddAsync(orderStatus);
        }

        public async Task UpdateOrderStatusAsync(OrderStatus orderStatus)
        {
            await _orderStatusRepository.UpdateAsync(orderStatus);
        }

        public async Task DeleteOrderStatusAsync(int orderStatusId)
        {
            await _orderStatusRepository.DeleteAsync(orderStatusId);
        }
    }
}