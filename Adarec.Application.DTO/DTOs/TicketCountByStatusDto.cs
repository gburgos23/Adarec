namespace Adarec.Application.DTO.DTOs
{
    public class TicketCountByStatusDto
    {
        public string Status { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int? TechnicianId { get; set; }
        public string? TechnicianName { get; set; }
        public int Count { get; set; }
    }
}