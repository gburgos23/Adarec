using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class CustomerRepositoryImpl(adarecContext context) : RepositoryImpl<Customer>(context), ICustomerRepository
    {
        private readonly adarecContext context = context;

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await context.Customers
                .Include(c => c.IdentificationType)
                .Where(c => c.Status)
                .ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdentificationAsync(string identificationClient)
        {
            return await context.Customers
                .Include(c => c.IdentificationType)
                .Where(c => c.Status && c.IdentificationNumber == identificationClient)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Customer>> ListOrdersByCustomerAsync()
        {
            return await context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderStatus)
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderAssignments)
                .ToListAsync();
        }
    }
}