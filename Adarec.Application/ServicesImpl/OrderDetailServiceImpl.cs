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

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int detailId)
        {
            return await _orderDetailRepository.GetByIdAsync(detailId);
        }

        public async Task AddOrderDetailAsync(OrderDetail detail)
        {
            await _orderDetailRepository.AddAsync(detail);
        }

        public async Task UpdateOrderDetailAsync(DeviceDetailDto detail)
        {
            try
            {
                if (detail == null || detail.DetailId == null || detail.DetailId <= 0)
                    throw new ArgumentException("No hay datos de detalle de dispositivo para actualizar.");

                var orderDetail = await _orderDetailRepository.GetByIdAsync(detail.DetailId.Value) ?? throw new InvalidOperationException("Detalle de dispositivo no encontrado.");
                orderDetail.ModelId = detail.ModelId;
                orderDetail.Quantity = detail.Quantity;
                orderDetail.IntakePhoto = detail.IntakePhoto;
                orderDetail.DeviceSpecs = detail.DeviceSpecs ?? string.Empty;
                orderDetail.ItemStatusId = detail.ItemStatusId;
                orderDetail.DateUpdated = DateTime.UtcNow;
                orderDetail.solution_photo = detail.SolutionPhoto;

                await _orderDetailRepository.UpdateAsync(orderDetail);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el detalle de dispositivo: {ex.Message}", ex);
            }
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