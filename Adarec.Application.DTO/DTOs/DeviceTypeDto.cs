using System.Text.Json.Serialization;

namespace Adarec.Application.DTO.DTOs
{
    public class DeviceTypeDto
    {
        [JsonPropertyName("id")]
        public int? DeviceTypeId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;
    }
}