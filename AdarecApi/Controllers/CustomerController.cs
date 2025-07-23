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
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _service.GetAllCustomersAsync();
                return StatusCode(StatusCodes.Status200OK, customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{identificationClient}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CustomerDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomersByIdentification([FromRoute] string identificationClient)
        {
            try
            {
                var customers = await _service.CustomersByIdentification(identificationClient);
                if (customers != null)
                {
                    return StatusCode(StatusCodes.Status200OK, customers);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No se encontro cliente con esa identificación.");
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
        /// <remarks>
        /// Crea un nuevo cliente en el sistema. El campo <c>CustomerId</c> debe ser nulo.
        /// </remarks>
        /// <param name="customer">Objeto con los datos del cliente a agregar.</param>
        /// <response code="201">Cliente agregado exitosamente.</response>
        /// <response code="400">Datos de cliente inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
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
        /// <remarks>
        /// Actualiza los datos de un cliente existente. El campo <c>CustomerId</c> debe ser mayor a 0.
        /// </remarks>
        /// <param name="customer">Objeto con los datos del cliente a actualizar.</param>
        /// <response code="204">Cliente actualizado exitosamente.</response>
        /// <response code="400">Datos de cliente inválidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDetailDto customer)
        {
            try
            {
                if (customer == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Datos de cliente inválidos.");

                await _service.UpdateCustomerAsync(customer);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina (inhabilita) un cliente por su ID.
        /// </summary>
        /// <remarks>
        /// Inhabilita el cliente especificado por su identificador.
        /// </remarks>
        /// <param name="customerId">ID del cliente a eliminar.</param>
        /// <response code="204">Cliente eliminado exitosamente.</response>
        /// <response code="404">No se encontró el cliente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete]
        [Route("{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int customerId)
        {
            try
            {
                await _service.DeleteCustomerAsync(customerId);
                return StatusCode(StatusCodes.Status204NoContent);
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
