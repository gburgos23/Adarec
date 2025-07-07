
namespace Adarec.Application.DTO.DTOs
{
    public class CustomerOrdersDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<SimpleOrderSummaryDto> Orders { get; set; }
    }
}
