using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class OrderRepositoryImpl(adarecContext context) : RepositoryImpl<Order>(context), IOrderRepository
    {
    }
}
