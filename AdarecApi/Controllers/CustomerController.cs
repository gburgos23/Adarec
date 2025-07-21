using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService service, IIdentificationTypeService identificationTypeService) : ControllerBase
    {
        private readonly ICustomerService _service = service;
        private readonly IIdentificationTypeService _identificationTypeService = identificationTypeService;

        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<CustomerDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _service.GetAllCustomersAsync();
                if (customers.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, customers);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron clientes.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Agrega un nuevo cliente.
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDetailDto customer)
        {
            try
            {
                if (customer == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Datos de cliente inválidos.");

                await _service.AddCustomerAsync(customer);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDetailDto customer)
        {
            try
            {
                if (customer == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Datos de cliente inválidos.");

                await _service.UpdateCustomerAsync(customer);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina (inhabilita) un cliente por su ID.
        /// </summary>
        [HttpDelete("{customerId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            try
            {
                await _service.DeleteCustomerAsync(customerId);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista las órdenes por cliente.
        /// </summary>
        [HttpGet("orders")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<CustomerOrdersDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListOrdersByCustomer()
        {
            try
            {
                var customers = await _service.ListOrdersByCustomerAsync();
                if (customers.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, customers);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron órdenes para los clientes.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los tipos de identificación.
        /// </summary>
        [HttpGet]
        [Route("identification-types")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<RolDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllIdentificationTypes()
        {
            try
            {
                var types = await _identificationTypeService.GetAllIdentificationTypesAsync();
                if (types.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, types);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontraron tipos de identificación.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
