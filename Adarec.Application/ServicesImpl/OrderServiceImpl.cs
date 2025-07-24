using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Application.ServicesImpl
{
    public class OrderServiceImpl(adarecContext context) : IOrderService
    {
        private readonly adarecContext _context = context;
        private readonly IOrderRepository _orderRepository = new OrderRepositoryImpl(context);

        public async Task<int> AddOrderAsync(OrderDto orderDto)
        {
            try
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

                return await _orderRepository.AddOrderAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al ingresar orden: {ex.Message}");
            }
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .Include(o => o.OrderAssignments)
                    .Include(o => o.Comments)
                    .FirstOrDefaultAsync(o => o.OrderId == orderDto.OrderId)
                    ?? throw new Exception("Orden no encontrada.");

                order.Description = orderDto.Description;
                order.ScheduledFor = orderDto.ScheduledFor;
                order.DateUpdate = DateTime.UtcNow;
                order.OrderStatusId = orderDto.OrderStatusId;

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.IdentificationNumber == orderDto.Customer.IdentificationNumber);
                if (customer != null)
                    order.CustomerId = customer.CustomerId;

                var incomingDetails = orderDto.Devices ?? [];

                var toRemove = order.OrderDetails
                    .Where(od => !incomingDetails.Any(d => d.DetailId == od.DetailId))
                    .ToList();
                foreach (var od in toRemove)
                    _context.OrderDetails.Remove(od);

                foreach (var d in incomingDetails)
                {
                    var existing = order.OrderDetails.FirstOrDefault(od => od.DetailId == d.DetailId);
                    if (existing != null)
                    {
                        existing.ModelId = d.ModelId;
                        existing.ItemStatusId = d.ItemStatusId;
                        existing.DeviceSpecs = d.DeviceSpecs;
                        existing.Quantity = d.Quantity;
                        existing.DateUpdated = DateTime.UtcNow;
                        existing.IntakePhoto = d.IntakePhoto;
                    }
                    else
                    {
                        order.OrderDetails.Add(new OrderDetail
                        {
                            OrderId = (int)order.OrderId!,
                            ModelId = d.ModelId,
                            ItemStatusId = d.ItemStatusId,
                            DeviceSpecs = d.DeviceSpecs,
                            Quantity = d.Quantity,
                            DateUpdated = DateTime.UtcNow,
                            IntakePhoto = d.IntakePhoto
                        });
                    }
                }

                if (orderDto.TechnicianId != null)
                {
                    var existingAssignment = order.OrderAssignments.FirstOrDefault();
                    if (existingAssignment != null)
                    {
                        existingAssignment.TechnicianId = orderDto.TechnicianId.Value;
                        existingAssignment.AssignedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        order.OrderAssignments.Add(new OrderAssignment
                        {
                            TechnicianId = orderDto.TechnicianId.Value,
                            AssignedAt = DateTime.UtcNow
                        });
                    }
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

                await _orderRepository.UpdateOrderAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la orden: {ex.Message}", ex);
            }
        }

        public async Task<List<TechnicianPendingOrdersDto>> ListPendingOrdersByTechnicianAsync(int idTechnician)
        {
            var orders = await _orderRepository.ListPendingOrdersByTechnicianAsync();

            var result = orders
                .SelectMany(o => o.OrderAssignments
                    .Where(oa => oa.TechnicianId == idTechnician && oa.Technician.Roles.Any(r => r.RoleId == 2)),
                    (o, oa) => new { o, oa })
                .GroupBy(x => x.oa.Technician.UserId)
                .Select(g => new TechnicianPendingOrdersDto
                {
                    TechnicianId = g.Key,
                    TechnicianName = g.Select(x => x.oa.Technician.Name).FirstOrDefault(),
                    PendingOrders = g.Select(x => new PendingOrderSummaryDto
                    {
                        OrderId = x.o.OrderId,
                        CustomerName = x.o.Customer.Name,
                        Description = x.o.Description,
                        CustomerIdentification = x.o.Customer.IdentificationNumber,
                        Status = x.o.OrderStatus.OrderStatusId,
                        StatusDescription = x.o.OrderStatus.Name,
                        ScheduledFor = x.o.ScheduledFor
                    }).ToList(),
                    Customer = g.Select(x => x.o.Customer)
                        .Select(c => new CustomerDetailDto
                        {
                            CustomerId = c.CustomerId,
                            Name = c.Name,
                            IdentificationNumber = c.IdentificationNumber,
                            Email = c.Email,
                            Phone = c.Phone,
                            Address = c.Address
                        }).FirstOrDefault()
                })
                .ToList();

            return result;
        }

        public async Task<OrderFullDetailDto?> GetOrderDetailByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderDetailByIdAsync(orderId);
            if (order == null) return null;

            var lastAssignment = order.OrderAssignments
                .OrderByDescending(oa => oa.AssignedAt)
                .FirstOrDefault();

            var technician = lastAssignment?.Technician;

            return new OrderFullDetailDto
            {
                OrderId = order.OrderId,
                Description = order.Description,
                Status = order.OrderStatus?.OrderStatusId,
                ScheduledFor = order.ScheduledFor,
                Customer = order.Customer == null ? null : new CustomerDetailDto
                {
                    CustomerId = order.Customer.CustomerId,
                    Name = order.Customer.Name,
                    IdentificationNumber = order.Customer.IdentificationNumber,
                    Email = order.Customer.Email,
                    Phone = order.Customer.Phone,
                    Address = order.Customer.Address
                },
                Devices = order.OrderDetails?.Select(od => new DeviceDetailDto
                {
                    DetailId = od.DetailId,
                    ModelId = od.ModelId,
                    ModelName = od.Model?.Name,
                    BrandName = od.Model?.Brand?.Name,
                    DeviceTypeName = od.Model?.DeviceType?.Name,
                    ItemStatusId = od.ItemStatusId,
                    ItemStatus = od.ItemStatus?.Name,
                    Quantity = od.Quantity,
                    IntakePhoto = od.IntakePhoto,
                    SolutionPhoto = od.solution_photo,
                    DeviceSpecs = od.DeviceSpecs
                }).ToList(),
                Technician = technician == null ? null : new TechnicianDto
                {
                    TechnicianId = technician.UserId,
                    Name = technician.Name,
                    Email = technician.Email
                },
                Comments = order.Comments?
                    .OrderByDescending(cm => cm.CreatedAt)
                    .Select(cm => new CommentDetailDto
                    {
                        CommentId = cm.CommentId,
                        UserName = cm.User?.Name,
                        Comment = cm.Comment1,
                        CreatedAt = cm.CreatedAt
                    }).ToList()
            };
        }

        public async Task<List<TechnicianPendingOrdersDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();

            var result = orders
                .SelectMany(o => o.OrderAssignments
                    .Where(oa => oa.Technician != null),
                    (o, oa) => new { o, oa })
                .GroupBy(x => x.oa.Technician.UserId)
                .Select(g => new TechnicianPendingOrdersDto
                {
                    TechnicianId = g.Key,
                    TechnicianName = g.Select(x => x.oa.Technician.Name).FirstOrDefault(),
                    PendingOrders = g.Select(x => new PendingOrderSummaryDto
                    {
                        OrderId = x.o.OrderId,
                        CustomerName = x.o.Customer.Name,
                        Description = x.o.Description,
                        CustomerIdentification = x.o.Customer.IdentificationNumber,
                        Status = x.o.OrderStatus.OrderStatusId,
                        StatusDescription = x.o.OrderStatus.Name,
                        ScheduledFor = x.o.ScheduledFor
                    }).ToList(),
                    Customer = g.Select(x => x.o.Customer)
                        .Select(c => new CustomerDetailDto
                        {
                            CustomerId = c.CustomerId,
                            Name = c.Name,
                            IdentificationNumber = c.IdentificationNumber,
                            Email = c.Email,
                            Phone = c.Phone,
                            Address = c.Address
                        }).FirstOrDefault()
                })
                .ToList();

            return result;
        }

        public async Task<List<TicketCountByStatusDto>> GetTicketCountByStatusAsync(int? year, int? month, int? technicianId = null)
        {
            var orders = await _orderRepository.GetTicketCountByStatusAsync(year, month, technicianId);

            var result = orders
                .SelectMany(o => o.OrderAssignments
                    .Where(oa => technicianId == null || oa.TechnicianId == technicianId),
                    (o, oa) => new { o, oa })
                .GroupBy(x => new
                {
                    StatusName = x.o.OrderStatus.Name,
                    Year = x.o.ScheduledFor?.Year ?? 0,
                    Month = x.o.ScheduledFor?.Month ?? 0,
                    TechnicianId = x.oa.TechnicianId,
                    TechnicianName = x.oa.Technician?.Name
                })
                .Select(g => new TicketCountByStatusDto
                {
                    Status = g.Key.StatusName,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TechnicianId = g.Key.TechnicianId,
                    TechnicianName = g.Key.TechnicianName,
                    Count = g.Count()
                })
                .ToList();

            return result;
        }

        public async Task<List<TechnicianWorkloadDto>> GetTechnicianWorkloadAsync()
        {
            var technicians = await _orderRepository.GetTechniciansAsync();

            return technicians.Select(u => new TechnicianWorkloadDto
            {
                TechnicianId = u.UserId,
                TechnicianName = u.Name,
                AssignedOrdersCount = u.OrderAssignments?.Count() ?? 0
            }).ToList();
        }
    }
}
