using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Adarec.Application.DTO.DTOs
{
    public class RolDto
    {
        [JsonPropertyName("id")]
        public int? RolId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;
    }
}
