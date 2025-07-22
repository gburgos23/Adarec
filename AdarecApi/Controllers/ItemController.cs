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
                return StatusCode(StatusCodes.Status200OK, "Detalle de dispositivo actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el detalle: {ex.Message}");
            }
        }
    }
}
