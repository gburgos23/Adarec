using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adarec.Application.DTO.DTOs
{
    public class ItemStatusDto
    {
        public int DetailId { get; set; }
        public string ItemStatus { get; set; }
        public DateTime StatusChangedAt { get; set; }
    }
}
