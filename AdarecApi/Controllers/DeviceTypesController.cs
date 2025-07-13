using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypesController(IDeviceTypeService service) : ControllerBase
    {
        private readonly IDeviceTypeService _service = service;

        /// <summary>
        /// Obtiene todos los tipos de dispositivos activos.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de todos los tipos de dispositivos activos en el sistema.
        /// </remarks>
        /// <response code="200">Lista de tipos de dispositivos encontrada.</response>
        /// <response code="404">No se encontraron tipos de dispositivos activos.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<DeviceTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDeviceTypes()
        {
            var types = await _service.GetActiveDeviceTypesAsync();
            if (types.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, types);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No active device types found.");
            }
        }

        /// <summary>
        /// Agrega un nuevo tipo de dispositivo.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo tipo de dispositivo. El campo <c>DeviceTypeId</c> debe ser nulo.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "name": "Tipo Ejemplo",
        ///   "status": true
        /// }
        /// </remarks>
        /// <param name="deviceType">Objeto DeviceTypeDto con los datos a agregar.</param>
        /// <response code="201">Tipo de dispositivo agregado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="500">Error interno.</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostDeviceType([FromBody] DeviceTypeDto deviceType)
        {
            if (deviceType == null || deviceType.DeviceTypeId != null)
            {
                return BadRequest("Invalid device type data.");
            }
            try
            {
                await _service.AddDeviceTypeAsync(deviceType);
                return StatusCode(StatusCodes.Status201Created, $"Device type {deviceType.Name} added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding device type: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un tipo de dispositivo existente.
        /// </summary>
        /// <remarks>
        /// Actualiza los datos de un tipo de dispositivo existente. El campo <c>DeviceTypeId</c> debe ser mayor a 0.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "deviceTypeId": 1,
        ///   "name": "Tipo Actualizado",
        ///   "status": true
        /// }
        /// </remarks>
        /// <param name="deviceType">Objeto DeviceTypeDto con los datos a actualizar.</param>
        /// <response code="200">Tipo de dispositivo actualizado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="500">Error interno.</response>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutDeviceType([FromBody] DeviceTypeDto deviceType)
        {
            if (deviceType == null || deviceType.DeviceTypeId <= 0)
            {
                return BadRequest("Invalid device type data.");
            }
            try
            {
                await _service.UpdateDeviceTypeAsync(deviceType);
                return StatusCode(StatusCodes.Status200OK, $"Device type {deviceType.Name} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating device type: {ex.Message}");
            }
        }
    }
}