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
            // Órdenes asignadas a técnicos con rol 2 y estado distinto de 3
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .Include(o => o.OrderAssignments)
                    .ThenInclude(oa => oa.Technician)
                .Include(o => o.OrderStatus)
                .Where(o => o.OrderAssignments.Any(oa => oa.Technician.Roles.Any(r => r.RoleId == 2)) && o.OrderStatusId != 3)
                .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
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
                .Include(o => o.Comments)
                    .ThenInclude(c => c.User)
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return order;
        }

        public async Task<List<Order>> GetOrderDetailByCustomerDocumentAsync(string identificationNumber)
        {
            var orders = await _context.Orders
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
                .Include(o => o.Comments)
                    .ThenInclude(c => c.User)
                .Include(o => o.OrderStatus)
                .Where(o => o.Customer.IdentificationNumber == identificationNumber)
                .ToListAsync();

            return orders;
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

        public async Task<Order?> GetOrderStatusHistoryAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderAssignments)
                    .ThenInclude(oa => oa.Technician)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.ItemStatus)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return order;
        }

        public async Task<List<Solution>> GetSolutionsByOrderAsync(int orderId)
        {
            var solutions = await _context.Solutions
                .Include(s => s.Order)
                .Include(s => s.Order.OrderAssignments)
                    .ThenInclude(oa => oa.Technician)
                .Where(s => s.OrderId == orderId)
                .OrderBy(s => s.ClosedAt)
                .ToListAsync();

            return solutions;
        }
    }
}
