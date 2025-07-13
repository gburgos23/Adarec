using System;
using System.Text.Json.Serialization;

namespace Adarec.Application.DTO.DTOs
{
    public class TechnicianDto
    {
        [JsonPropertyName("id")]
        public int? TechnicianId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("idRol")]
        public List<int?> IdRol { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;
    }
}
