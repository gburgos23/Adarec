using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class OrderAssignmentRepositoryImpl(adarecContext context) : RepositoryImpl<OrderAssignment>(context), IOrderAssignmentRepository
    {
    }
}