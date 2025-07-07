using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        [OperationContract]
        Task<Customer?> GetCustomerByIdAsync(int customerId);

        [OperationContract]
        Task AddCustomerAsync(Customer customer);

        [OperationContract]
        Task UpdateCustomerAsync(Customer customer);

        [OperationContract]
        Task DeleteCustomerAsync(int customerId);

        [OperationContract]
        Task<List<CustomerOrdersDto>> ListOrdersByCustomerAsync();
    }
}