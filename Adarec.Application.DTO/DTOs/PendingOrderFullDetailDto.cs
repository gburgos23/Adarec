namespace Adarec.Application.DTO.DTOs
{
    public class PendingOrderFullDetailDto
    {
        public int? OrderId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? ScheduledFor { get; set; }
        public CustomerDetailDto Customer { get; set; }
        public List<DeviceDetailDto> Devices { get; set; }
        public TechnicianDto? Technician { get; set; }
    }
}