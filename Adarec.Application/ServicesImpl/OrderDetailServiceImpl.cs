using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class OrderDetailServiceImpl(adarecContext context) : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository = new OrderDetailRepositoryImpl(context);

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _orderDetailRepository.GetAllAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int detailId)
        {
            return await _orderDetailRepository.GetByIdAsync(detailId);
        }

        public async Task AddOrderDetailAsync(OrderDetail detail)
        {
            await _orderDetailRepository.AddAsync(detail);
        }

        public async Task UpdateOrderDetailAsync(OrderDetail detail)
        {
            await _orderDetailRepository.UpdateAsync(detail);
        }

        public async Task DeleteOrderDetailAsync(int detailId)
        {
            await _orderDetailRepository.DeleteAsync(detailId);
        }

        public async Task<List<PendingOrderFullDetailDto>> GetAllPendingOrdersWithDetailsAsync()
        {
            return await _orderDetailRepository.GetAllPendingOrdersWithDetailsAsync();
        }
    }
}