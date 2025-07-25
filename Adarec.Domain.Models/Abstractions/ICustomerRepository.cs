using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<CustomerOrdersDto>> ListOrdersByCustomerAsync();
    }
}