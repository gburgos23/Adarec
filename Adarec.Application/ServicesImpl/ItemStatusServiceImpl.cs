using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class ItemStatusServiceImpl(adarecContext context) : IItemStatusService
    {
        private readonly IItemStatusRepository _itemStatusRepository = new ItemStatusRepositoryImpl(context);

        public async Task<List<RolDto>> GetAllItemStatusesAsync()
        {
            var data = await _itemStatusRepository.GetAllAsync();

            return [.. data.Select(x => new RolDto
            {
                RolId = x.ItemStatusId,
                Name = x.Name,
                Status = x.Status
            })];
        }
    }
}