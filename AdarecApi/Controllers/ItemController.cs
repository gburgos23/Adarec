using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(IItemStatusService itemStatusService, IOrderDetailService orderDetailService) : ControllerBase
    {
        private readonly IItemStatusService _itemStatusService = itemStatusService;
        private readonly IOrderDetailService _orderDetailService = orderDetailService;

        /// <summary>
        /// Obtiene todos los estados de item de orden.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de estados de item de orden disponibles.
        /// </remarks>
        /// <response code="200">Lista de estados encontrada.</response>
        [HttpGet("statuses")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<RolDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrderStatuses()
        {
            var result = await _itemStatusService.GetAllItemStatusesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Actualiza un detalle de dispositivo existente.
        /// </summary>
        /// <remarks>
        /// Actualiza los datos de un detalle de dispositivo. El campo <c>DetailId</c> debe ser mayor a 0.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "detailId": 12,
        ///   "modelId": 10,
        ///   "itemStatusId": 2,
        ///   "quantity": 1,
        ///   "deviceSpecs": "HP LaserJet Pro, WiFi",
        ///   "intakePhoto": "base64string"
        /// }
        /// </remarks>
        /// <param name="deviceDetail">Objeto DeviceDetailDto con los datos a actualizar.</param>
        /// <response code="200">Detalle actualizado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="500">Error interno al actualizar el detalle.</response>
        [HttpPut]
        [Route("device-detail")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutDeviceDetail([FromBody] DeviceDetailDto deviceDetail)
        {
            if (deviceDetail == null || deviceDetail.DetailId == null || deviceDetail.DetailId <= 0)
                return BadRequest("Datos de dispositivo inválidos.");

            try
            {
                await _orderDetailService.UpdateOrderDetailAsync(deviceDetail);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el detalle: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserta una lista de detalles de dispositivos para una orden.
        /// </summary>
        /// <remarks>
        /// Inserta múltiples detalles de dispositivos asociados a una orden. No se debe enviar <c>DetailId</c>.
        /// 
        /// <b>Ejemplo de body:</b>
        /// [
        ///   {
        ///     "orderId": 5,
        ///     "modelId": 10,
        ///     "itemStatusId": 2,
        ///     "quantity": 1,
        ///     "deviceSpecs": "HP LaserJet Pro, WiFi",
        ///     "intakePhoto": "base64string"
        ///   }
        /// ]
        /// </remarks>
        /// <param name="deviceDetails">Lista de DeviceDetailDto a insertar.</param>
        /// <response code="201">Detalles insertados exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="500">Error interno al insertar los detalles.</response>
        [HttpPost("device-details")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostDeviceDetails([FromBody] DeviceDetailDto detail)
        {
            if (detail.OrderId == null || detail.OrderId <= 0)
                return BadRequest("Todos los dispositivos deben tener un OrderId válido.");
            if (detail.DetailId != null && detail.DetailId > 0)
                return BadRequest("No se debe enviar DetailId para inserción.");

            try
            {
                await _orderDetailService.AddOrderDetailAsync(detail);
                return StatusCode(StatusCodes.Status201Created, "Detalles de dispositivos insertados exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al insertar los detalles: {ex.Message}");
            }
        }
    }
}
