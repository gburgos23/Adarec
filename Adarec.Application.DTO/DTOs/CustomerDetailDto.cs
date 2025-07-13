using System;

namespace Adarec.Application.DTO.DTOs
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public int? IdentificationTypeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
