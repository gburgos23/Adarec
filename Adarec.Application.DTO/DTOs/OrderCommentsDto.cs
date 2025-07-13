namespace Adarec.Application.DTO.DTOs
{
    public class OrderCommentsDto
    {
        public int? OrderId { get; set; }
        public string? OrderDescription { get; set; }
        public List<CommentDetailDto> Comments { get; set; } = [];
    }
}
