using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class OrderStatusRepositoryImpl(adarecContext context) : RepositoryImpl<OrderStatus>(context), IOrderStatusRepository
    {
    }
}