using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<List<PendingOrderFullDetailDto>> GetAllPendingOrdersWithDetailsAsync();
    }
}