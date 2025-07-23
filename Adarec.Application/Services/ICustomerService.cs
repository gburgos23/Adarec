using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Task<List<CustomerDetailDto>> GetAllCustomersAsync();

        [OperationContract]
        Task<CustomerDetailDto?> CustomersByIdentification(string identificationClient);

        [OperationContract]
        Task AddCustomerAsync(CustomerDetailDto customer);

        [OperationContract]
        Task UpdateCustomerAsync(CustomerDetailDto customer);

        [OperationContract]
        Task DeleteCustomerAsync(int customerId);
    }
}