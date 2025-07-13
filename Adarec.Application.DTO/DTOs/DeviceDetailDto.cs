namespace Adarec.Application.DTO.DTOs
{
    public class DeviceDetailDto
    {
        public int? DetailId { get; set; }
        public int? ModelId { get; set; }
        public string? ModelName { get; set; }
        public string? BrandName { get; set; }
        public string? DeviceTypeName { get; set; }
        public int? ItemStatusId { get; set; }
        public string? ItemStatus { get; set; }
        public int Quantity { get; set; }
        public string? IntakePhoto { get; set; }
        public string? DeviceSpecs { get; set; }
    }
}
