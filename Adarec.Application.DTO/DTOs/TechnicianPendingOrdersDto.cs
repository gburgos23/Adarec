namespace Adarec.Application.DTO.DTOs
{
    public class TechnicianPendingOrdersDto
    {
        public int? TechnicianId { get; set; }
        public List<PendingOrderSummaryDto>? PendingOrders { get; set; }
        public CustomerDetailDto? Customer { get; set; }
    }
}
