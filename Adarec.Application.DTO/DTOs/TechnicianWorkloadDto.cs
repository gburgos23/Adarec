namespace Adarec.Application.DTO.DTOs
{
    public class TechnicianWorkloadDto
    {
        public int? TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianEmail { get; set; }
        public int AssignedOrdersCount { get; set; }
    }
}