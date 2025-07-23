using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class OrderDetailRepositoryImpl(adarecContext context) : RepositoryImpl<OrderDetail>(context), IOrderDetailRepository
    {
        private readonly adarecContext _context = context;
    }
}