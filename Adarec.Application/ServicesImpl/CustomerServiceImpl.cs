using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class CustomerServiceImpl(adarecContext context) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = new CustomerRepositoryImpl(context);

        public async Task<List<CustomerDetailDto>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomersAsync();

                return customers.Select(c => new CustomerDetailDto
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    IdentificationNumber = c.IdentificationNumber,
                    IdentificationTypeId = c.IdentificationTypeId,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddCustomerAsync(CustomerDetailDto customerDto)
        {
            try
            {
                var customer = new Customer
                {
                    Name = customerDto.Name,
                    IdentificationNumber = customerDto.IdentificationNumber,
                    IdentificationTypeId = customerDto.IdentificationTypeId,
                    Email = customerDto.Email,
                    Phone = customerDto.Phone,
                    Address = customerDto.Address,
                    Status = true
                };

                await _customerRepository.AddAsync(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCustomerAsync(CustomerDetailDto customerDto)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerId = customerDto.CustomerId,
                    Name = customerDto.Name,
                    IdentificationNumber = customerDto.IdentificationNumber,
                    IdentificationTypeId = customerDto.IdentificationTypeId,
                    Email = customerDto.Email,
                    Phone = customerDto.Phone,
                    Address = customerDto.Address,
                    Status = true
                };

                await _customerRepository.UpdateAsync(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer is not null)
                {
                    customer.Status = false; 
                    await _customerRepository.UpdateAsync(customer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CustomerOrdersDto>> ListOrdersByCustomerAsync()
        {
            try
            {
                var customers = await _customerRepository.ListOrdersByCustomerAsync();

                var result = customers.Select(c => new CustomerOrdersDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.Name,
                    Orders = c.Orders?.Select(o => new SimpleOrderSummaryDto
                    {
                        OrderId = o.OrderId ?? 0,
                        Description = o.Description,
                        Status = o.OrderStatus != null ? o.OrderStatus.Name : "Sin estado",
                        TechnicianName = o.OrderAssignments != null && o.OrderAssignments.Count != 0
                            ? o.OrderAssignments.OrderByDescending(a => a.AssignedAt).FirstOrDefault()?.Technician?.Name ?? "No asignado"
                            : "No asignado",
                        ScheduledFor = o.ScheduledFor
                    }).ToList() ?? new List<SimpleOrderSummaryDto>()
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}