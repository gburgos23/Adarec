using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adarec.Application.DTO.DTOs
{
    public class OrderStatusSummaryDto
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public TechnicianDto? Technician { get; set; }
        public List<ItemStatusDto> Items { get; set; }
    }
}
