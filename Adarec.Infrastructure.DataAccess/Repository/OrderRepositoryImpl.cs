using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class OrderRepositoryImpl(adarecContext context) : RepositoryImpl<Order>(context), IOrderRepository
    {
        private readonly adarecContext _context = context;

        public async Task<int> AddOrderAsync(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return order.OrderId!.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.ToString());
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> ListPendingOrdersByTechnicianAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderAssignments)
                    .ThenInclude(oa => oa.Technician)
                        .ThenInclude(t => t.Roles)
                .Include(o => o.OrderStatus)
                .Where(o => o.OrderAssignments.Any(oa => oa.Technician.Roles.Any(r => r.RoleId == 2)))
                .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderAssignments)
                    .ThenInclude(oa => oa.Technician)
                .Include(o => o.OrderStatus)
                .ToListAsync();

            return orders;
        }

        public async Task<Order?> GetOrderDetailByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Model)
                        .ThenInclude(m => m.Brand)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Model)
                        .ThenInclude(m => m.DeviceType)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.ItemStatus)
                .Include(o => o.OrderAssignments)
                    .ThenInclude(oa => oa.Technician)
                .Include(o => o.Comments)
                    .ThenInclude(c => c.User)
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return order;
        }

        public async Task<List<Order>> GetTicketCountByStatusAsync(int year, int month, int? technicianId = null)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderAssignments)
                    .ThenInclude(oa => oa.Technician)
                .Where(o => o.ScheduledFor.HasValue
                    && o.ScheduledFor.Value.Year == year
                    && o.ScheduledFor.Value.Month == month
                    && o.OrderAssignments.Any(oa => technicianId == null || oa.TechnicianId == technicianId))
                .ToListAsync();

            return orders;
        }

        public async Task<List<User>> GetTechniciansAsync()
        {
            return await _context.Users
                .Include(u => u.OrderAssignments)
                .Include(u => u.Roles)
                .Where(u => u.Status && u.Roles.Any(r => r.RoleId == 2))
                .ToListAsync();
        }
    }
}
