using System.Text.Json.Serialization;

namespace Adarec.Application.DTO.DTOs
{
    public class BrandDto
    {
        [JsonPropertyName("id")]
        public int? BrandId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;
    }
}