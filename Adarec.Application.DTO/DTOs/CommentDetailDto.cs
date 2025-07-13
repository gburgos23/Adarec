using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adarec.Application.DTO.DTOs
{
    public class CommentDetailDto
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
