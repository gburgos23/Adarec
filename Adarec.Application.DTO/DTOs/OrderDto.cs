using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adarec.Application.DTO.DTOs
{
    public class OrderDto
    {
        public int? OrderId { get; set; }
        public string Description { get; set; }
        public DateTime? ScheduledFor { get; set; }
        public int OrderStatusId { get; set; }
        public CustomerDetailDto Customer { get; set; }
        public List<DeviceDetailDto> Devices { get; set; }
        public int? TechnicianId { get; set; }
        public CommentDetailDto? LastComment { get; set; }
    }
}
