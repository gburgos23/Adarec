using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class OrderStatusServiceImpl(adarecContext context) : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository = new OrderStatusRepositoryImpl(context);

        public async Task<List<RolDto>> GetAllOrderStatusesAsync()
        {
            var data = await _orderStatusRepository.GetAllAsync();
            return [.. data.Select(x => new RolDto
            {
                RolId = x.OrderStatusId,
                Name = x.Name,
                Status = x.Status
            }).Where(x => x.Status)];
        }
    }
}