using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class CustomerRepositoryImpl(adarecContext context) : RepositoryImpl<Customer>(context), ICustomerRepository
    {
        private readonly adarecContext context = context;

        public async Task<List<CustomerOrdersDto>> ListOrdersByCustomerAsync()
        {
            try
            {
                var result = await (
                    from c in context.Customers
                    join o in context.Orders on c.CustomerId equals o.CustomerId
                    join os in context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                    join oa in context.OrderAssignments on o.OrderId equals oa.OrderId into oaGroup
                    from oa in oaGroup.OrderByDescending(x => x.AssignedAt).Take(1).DefaultIfEmpty()
                    join t in context.Users on oa.TechnicianId equals t.UserId into tGroup
                    from t in tGroup.DefaultIfEmpty()
                    group new { o, os, t } by new { c.CustomerId, c.Name } into grupo
                    select new CustomerOrdersDto
                    {
                        CustomerId = grupo.Key.CustomerId,
                        CustomerName = grupo.Key.Name,
                        Orders = grupo.Select(x => new SimpleOrderSummaryDto
                        {
                            OrderId = x.o.OrderId,
                            Description = x.o.Description,
                            Status = x.os.Name,
                            TechnicianName = x.t != null ? x.t.Name : "No asignado",
                            ScheduledFor = x.o.ScheduledFor
                        }).ToList()
                    }
                ).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customer orders: {ex.Message}", ex);
            }
        }
    }
}