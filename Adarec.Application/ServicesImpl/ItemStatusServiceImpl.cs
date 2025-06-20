using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class ItemStatusServiceImpl(adarecContext context) : IItemStatusService
    {
        private readonly IItemStatusRepository _itemStatusRepository = new ItemStatusRepositoryImpl(context);

        public async Task<IEnumerable<ItemStatus>> GetAllItemStatusesAsync()
        {
            return await _itemStatusRepository.GetAllAsync();
        }

        public async Task<ItemStatus?> GetItemStatusByIdAsync(int itemStatusId)
        {
            return await _itemStatusRepository.GetByIdAsync(itemStatusId);
        }

        public async Task AddItemStatusAsync(ItemStatus itemStatus)
        {
            await _itemStatusRepository.AddAsync(itemStatus);
        }

        public async Task UpdateItemStatusAsync(ItemStatus itemStatus)
        {
            await _itemStatusRepository.UpdateAsync(itemStatus);
        }

        public async Task DeleteItemStatusAsync(int itemStatusId)
        {
            await _itemStatusRepository.DeleteAsync(itemStatusId);
        }
    }
}