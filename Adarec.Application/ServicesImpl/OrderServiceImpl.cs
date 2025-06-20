using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class OrderServiceImpl(adarecContext context) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = new OrderRepositoryImpl(context);

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteAsync(orderId);
        }
    }
}
