using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class OrderDetailRepositoryImpl(adarecContext context) : RepositoryImpl<OrderDetail>(context), IOrderDetailRepository
    {
        public async Task<List<PendingOrderFullDetailDto>> GetAllPendingOrdersWithDetailsAsync()
        {
            var result = await (
                from o in context.Orders
                where o.OrderStatusId != 3 // 3 = Finalizado
                join c in context.Customers on o.CustomerId equals c.CustomerId
                join os in context.OrderStatuses on o.OrderStatusId equals os.OrderStatusId
                // Técnico asignado (última asignación)
                join oa in context.OrderAssignments on o.OrderId equals oa.OrderId into oaGroup
                from oa in oaGroup.OrderByDescending(x => x.AssignedAt).Take(1).DefaultIfEmpty()
                join t in context.Users on oa.TechnicianId equals t.UserId into tGroup
                from t in tGroup.DefaultIfEmpty()
                select new PendingOrderFullDetailDto
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
                    } : null
                }
            ).ToListAsync();

            return result;
        }
    }
}