using System;

namespace Adarec.Application.DTO.DTOs
{
    public class SimpleOrderSummaryDto
    {
        public int? OrderId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string TechnicianName { get; set; }
        public DateTime? ScheduledFor { get; set; }
    }
}
