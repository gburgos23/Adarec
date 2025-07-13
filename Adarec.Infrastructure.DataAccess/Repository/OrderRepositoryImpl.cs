using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class OrderRepositoryImpl : RepositoryImpl<Order>, IOrderRepository
    {
        private readonly adarecContext _context;

        public OrderRepositoryImpl(adarecContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> AddOrderAsync(OrderDto orderDto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.IdentificationNumber == orderDto.Customer.IdentificationNumber);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = orderDto.Customer.Name,
                    IdentificationNumber = orderDto.Customer.IdentificationNumber,
                    Email = orderDto.Customer.Email,
                    Phone = orderDto.Customer.Phone,
                    Address = orderDto.Customer.Address,
                    IdentificationTypeId = orderDto.Customer.IdentificationTypeId,
                    Status = true
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var order = new Order
            {
                Description = orderDto.Description,
                OrderStatusId = orderDto.OrderStatusId,
                ScheduledFor = orderDto.ScheduledFor,
                CustomerId = customer.CustomerId,
                DateUpdate = DateTime.UtcNow
            };

            if (orderDto.Devices != null && orderDto.Devices.Count > 0)
            {
                order.OrderDetails = [.. orderDto.Devices.Select(d => new OrderDetail
                {
                    Quantity = d.Quantity,
                    ModelId = d.ModelId,
                    ItemStatusId = d.ItemStatusId,
                    DeviceSpecs = d.DeviceSpecs,
                    IntakePhoto = d.IntakePhoto,
                    DateUpdated = DateTime.UtcNow
                })];
            }

            if (orderDto.TechnicianId != null)
            {
                order.OrderAssignments =
                [
                    new OrderAssignment
                    {
                        TechnicianId = orderDto.TechnicianId,
                        AssignedAt = DateTime.UtcNow
                    }
                ];
            }

            if (orderDto.LastComment != null)
            {
                order.Comments =
                [
                    new() {
                        Comment1 = orderDto.LastComment.Comment,
                        CreatedAt = DateTime.UtcNow,
                        UserId = orderDto.LastComment.UserId
                    }
                ];
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.OrderId!.Value;
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.OrderAssignments)
                .Include(o => o.Comments)
                .FirstOrDefaultAsync(o => o.OrderId == orderDto.OrderId) ?? throw new Exception("Orden no encontrada.");
            order.Description = orderDto.Description;
            order.ScheduledFor = orderDto.ScheduledFor;
            order.DateUpdate = DateTime.UtcNow;
            order.OrderStatusId = orderDto.OrderStatusId;
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.IdentificationNumber == orderDto.Customer.IdentificationNumber);
            if (customer != null)
                order.CustomerId = customer.CustomerId;

            order.OrderDetails.Clear();
            if (orderDto.Devices != null && orderDto.Devices.Count > 0)
            {
                foreach (var d in orderDto.Devices)
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        ModelId = d.ModelId,
                        ItemStatusId = d.ItemStatusId,
                        DeviceSpecs = d.DeviceSpecs,
                        Quantity = d.Quantity,
                        DateUpdated = DateTime.UtcNow,
                        IntakePhoto = d.IntakePhoto
                    });
                }
            }

            order.OrderAssignments.Clear();
            if (orderDto.TechnicianId != null)
            {
                order.OrderAssignments.Add(new OrderAssignment
                {
                    TechnicianId = orderDto.TechnicianId,
                    AssignedAt = DateTime.UtcNow
                });
            }

            if (orderDto.LastComment != null)
            {
                order.Comments.Add(new Comment
                {
                    Comment1 = orderDto.LastComment.Comment,
                    CreatedAt = DateTime.UtcNow,
                    UserId = orderDto.LastComment.UserId
                });
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<TechnicianPendingOrdersDto>> ListPendingOrdersByTechnicianAsync()
        {
            try
            {
                var result = await (
                    from t in _context.Users
                    where t.Roles.Any(r => r.RoleId == 2)
                    join oa in _context.OrderAssignments on t.UserId equals oa.TechnicianId
                    join o in _context.Orders on oa.OrderId equals o.OrderId
                    join c in _context.Customers on o.CustomerId equals c.CustomerId
                    join os in _context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                    where o.OrderStatusId != 3
                    group new { t, o, c, os } by new { t.UserId, t.Name } into grupo
                    select new TechnicianPendingOrdersDto
                    {
                        TechnicianName = grupo.Key.Name,
                        PendingOrders = grupo.Select(g => new PendingOrderSummaryDto
                        {
                            OrderId = g.o.OrderId,
                            CustomerName = g.c.Name,
                            Description = g.o.Description,
                            Status = g.os.Name,
                            ScheduledFor = g.o.ScheduledFor
                        }).ToList()
                    }
                ).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving pending orders by technician: {ex.Message}", ex);
            }
        }

        public async Task<OrderFullDetailDto?> GetOrderDetailByIdAsync(int orderId)
        {
            var result = await (
                from o in _context.Orders
                where o.OrderId == orderId
                join c in _context.Customers on o.CustomerId equals c.CustomerId
                join os in _context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                // Técnico asignado (última asignación)
                join oa in _context.OrderAssignments on o.OrderId equals oa.OrderId into oaGroup
                from oa in oaGroup.OrderByDescending(x => x.AssignedAt).Take(1).DefaultIfEmpty()
                join t in _context.Users on oa.TechnicianId equals t.UserId into tGroup
                from t in tGroup.DefaultIfEmpty()
                select new OrderFullDetailDto
                {
                    OrderId = o.OrderId,
                    Description = o.Description,
                    Status = os.Name,
                    ScheduledFor = o.ScheduledFor,
                    Customer = new CustomerDetailDto
                    {
                        CustomerId = c.CustomerId,
                        Name = c.Name,
                        IdentificationNumber = c.IdentificationNumber,
                        Email = c.Email,
                        Phone = c.Phone,
                        Address = c.Address
                    },
                    Devices = o.OrderDetails.Select(od => new DeviceDetailDto
                    {
                        DetailId = od.DetailId,
                        ModelName = od.Model.Name,
                        BrandName = od.Model.Brand.Name,
                        DeviceTypeName = od.Model.DeviceType.Name,
                        ItemStatus = od.ItemStatus.Name,
                        Quantity = od.Quantity,
                        IntakePhoto = od.IntakePhoto,
                        DeviceSpecs = od.DeviceSpecs
                    }).ToList(),
                    Technician = t != null ? new TechnicianDto
                    {
                        TechnicianId = t.UserId,
                        Name = t.Name,
                        Email = t.Email
                    } : null,
                    LastComment = o.Comments
                        .OrderByDescending(cm => cm.CreatedAt)
                        .Select(cm => new CommentDetailDto
                        {
                            CommentId = cm.CommentId,
                            UserName = cm.User.Name,
                            Comment = cm.Comment1,
                            CreatedAt = cm.CreatedAt
                        })
                        .FirstOrDefault()
                }
            ).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<OrderFullDetailDto>> GetOrderDetailByCustomerDocumentAsync(string identificationNumber)
        {
            var result = await (
                from o in _context.Orders
                join c in _context.Customers on o.CustomerId equals c.CustomerId
                join os in _context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                where c.IdentificationNumber == identificationNumber
                // Técnico asignado (última asignación)
                join oa in _context.OrderAssignments on o.OrderId equals oa.OrderId into oaGroup
                from oa in oaGroup.OrderByDescending(x => x.AssignedAt).Take(1).DefaultIfEmpty()
                join t in _context.Users on oa.TechnicianId equals t.UserId into tGroup
                from t in tGroup.DefaultIfEmpty()
                select new OrderFullDetailDto
                {
                    OrderId = o.OrderId,
                    Description = o.Description,
                    Status = os.Name,
                    ScheduledFor = o.ScheduledFor,
                    Customer = new CustomerDetailDto
                    {
                        CustomerId = c.CustomerId,
                        Name = c.Name,
                        IdentificationNumber = c.IdentificationNumber,
                        Email = c.Email,
                        Phone = c.Phone,
                        Address = c.Address
                    },
                    Devices = o.OrderDetails.Select(od => new DeviceDetailDto
                    {
                        DetailId = od.DetailId,
                        ModelName = od.Model.Name,
                        BrandName = od.Model.Brand.Name,
                        DeviceTypeName = od.Model.DeviceType.Name,
                        ItemStatus = od.ItemStatus.Name,
                        Quantity = od.Quantity,
                        IntakePhoto = od.IntakePhoto,
                        DeviceSpecs = od.DeviceSpecs
                    }).ToList(),
                    Technician = t != null ? new TechnicianDto
                    {
                        TechnicianId = t.UserId,
                        Name = t.Name,
                        Email = t.Email
                    } : null,
                    LastComment = o.Comments
                        .OrderByDescending(cm => cm.CreatedAt)
                        .Select(cm => new CommentDetailDto
                        {
                            CommentId = cm.CommentId,
                            UserName = cm.User.Name,
                            Comment = cm.Comment1,
                            CreatedAt = cm.CreatedAt
                        })
                        .FirstOrDefault()
                }
            ).ToListAsync();

            return result;
        }

        public async Task<List<TicketCountByStatusDto>> GetTicketCountByStatusAsync(int year, int month, int? technicianId = null)
        {
            var query =
                from o in _context.Orders
                join os in _context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                join oa in _context.OrderAssignments on o.OrderId equals oa.OrderId
                join t in _context.Users on oa.TechnicianId equals t.UserId
                where o.ScheduledFor.HasValue
                      && o.ScheduledFor.Value.Year == year
                      && o.ScheduledFor.Value.Month == month
                      && (technicianId == null || t.UserId == technicianId)
                group new { o, os, t } by new
                {
                    StatusName = os.Name,
                    Year = o.ScheduledFor.Value.Year,
                    Month = o.ScheduledFor.Value.Month,
                    TechnicianId = t.UserId,
                    TechnicianName = t.Name
                } into g
                select new TicketCountByStatusDto
                {
                    Status = g.Key.StatusName,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TechnicianId = g.Key.TechnicianId,
                    TechnicianName = g.Key.TechnicianName,
                    Count = g.Count()
                };

            return await query.ToListAsync();
        }

        public async Task<OrderStatusSummaryDto?> GetOrderStatusHistoryAsync(int orderId)
        {
            var result = await (
                from o in _context.Orders
                where o.OrderId == orderId
                join os in _context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                // Última asignación de técnico
                join oa in _context.OrderAssignments on o.OrderId equals oa.OrderId into oaGroup
                from oa in oaGroup.OrderByDescending(x => x.AssignedAt).Take(1).DefaultIfEmpty()
                join t in _context.Users on oa.TechnicianId equals t.UserId into tGroup
                from t in tGroup.DefaultIfEmpty()
                select new OrderStatusSummaryDto
                {
                    OrderId = o.OrderId,
                    OrderStatus = os.Name,
                    Technician = t != null ? new TechnicianDto
                    {
                        TechnicianId = t.UserId,
                        Name = t.Name,
                        Email = t.Email
                    } : null,
                    Items = o.OrderDetails.Select(od => new ItemStatusDto
                    {
                        DetailId = od.DetailId,
                        ItemStatus = od.ItemStatus.Name,
                        StatusChangedAt = od.DateUpdated ?? DateTime.MinValue
                    }).ToList()
                }
            ).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<SolutionDetailDto>> GetSolutionsByOrderAsync(int orderId)
        {
            // Última asignación de técnico en la orden
            var lastAssignment = await _context.OrderAssignments
                .Where(oa => oa.OrderId == orderId)
                .OrderByDescending(oa => oa.AssignedAt)
                .FirstOrDefaultAsync();

            string technicianName = string.Empty;

            if (lastAssignment != null)
            {
                var technician = await _context.Users
                    .Where(u => u.UserId == lastAssignment.TechnicianId)
                    .FirstOrDefaultAsync();

                technicianName = technician?.Name ?? "No asignado";
            }

            var result = await (
                from s in _context.Solutions
                where s.OrderId == orderId
                orderby s.ClosedAt
                select new SolutionDetailDto
                {
                    SolutionId = s.SolutionId,
                    TechnicianName = technicianName,
                    Description = s.Description,
                    FinalPhoto = s.FinalPhoto,
                    AppliedAt = s.ClosedAt
                }
            ).ToListAsync();

            return result;
        }
    }
}
