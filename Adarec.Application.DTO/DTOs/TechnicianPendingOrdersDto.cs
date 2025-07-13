namespace Adarec.Application.DTO.DTOs
{
    public class TechnicianPendingOrdersDto
    {
        public string? TechnicianName { get; set; }
        public List<PendingOrderSummaryDto>? PendingOrders { get; set; }
    }
}
