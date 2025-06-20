using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class ItemStatusRepositoryImpl(adarecContext context) : RepositoryImpl<ItemStatus>(context), IItemStatusRepository
    {
    }
}