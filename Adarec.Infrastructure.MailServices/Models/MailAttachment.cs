using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adarec.Infrastructure.MailServices.Models
{
    public class MailAttachment
    {
        public string FileName { get; set; } = default!;
        public byte[] Content { get; set; } = default!;
        public string MimeType { get; set; } = "application/octet-stream";
    }
}
