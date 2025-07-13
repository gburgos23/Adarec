using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService service) : ControllerBase
    {
        private readonly IOrderService _service = service;

        /// <summary>
        /// Agrega una nueva orden.
        /// </summary>
        /// <remarks>
        /// Crea una nueva orden en el sistema. El campo <c>OrderId</c> debe ser nulo.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "description": "Instalación de impresora multifunción",
        ///   "scheduledFor": "2025-07-15T09:00:00Z",
        ///   "orderStatusId": 1,
        ///   "customer": {
        ///     "name": "Ana Torres",
        ///     "identificationNumber": "87654321",
        ///     "email": "ana.torres@email.com",
        ///     "phone": "555123456",
        ///     "address": "Calle Falsa 456"
        ///   },
        ///   "devices": [
        ///     {
        ///       "modelId": 10,
        ///       "itemStatusId": 1,
        ///       "quantity": 1,
        ///       "deviceSpecs": "HP LaserJet Pro, WiFi",
        ///       "intakePhoto": "base64string"
        ///     }
        ///   ],
        ///   "technicianId": 3,
        ///   "lastComment": {
        ///     "comment": "Cliente solicita instalación urgente",
        ///     "userId": 2
        ///   }
        /// }
        /// </remarks>
        /// <param name="order">Objeto OrderDto con los datos de la orden a agregar.</param>
        /// <response code="201">Orden agregada exitosamente.</response>
        /// <response code="400">Datos de orden inválidos.</response>
        /// <response code="500">Error interno al agregar la orden.</response>
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostOrder([FromBody] OrderDto order)
        {
            if (order == null || order.OrderId != null) 
                return BadRequest("Datos de orden inválidos.");
            try
            {
                var id = await _service.AddOrderAsync(order);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    Message = "Orden agregada exitosamente.",
                    OrderId = id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar la orden: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una orden existente.
        /// </summary>
        /// <remarks>
        /// Actualiza los datos de una orden existente. El campo <c>OrderId</c> debe ser mayor a 0.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "orderId": 25,
        ///   "description": "Instalación de impresora multifunción y configuración de red",
        ///   "scheduledFor": "2025-07-16T10:00:00Z",
        ///   "orderStatusId": 2,
        ///   "customer": {
        ///     "customerId": 5,
        ///     "name": "Ana Torres",
        ///     "identificationNumber": "87654321",
        ///     "email": "ana.torres@email.com",
        ///     "phone": "555123456",
        ///     "address": "Calle Falsa 456"
        ///   },
        ///   "devices": [
        ///     {
        ///       "detailId": 12,
        ///       "modelId": 10,
        ///       "itemStatusId": 2,
        ///       "quantity": 1,
        ///       "deviceSpecs": "HP LaserJet Pro, WiFi",
        ///       "intakePhoto": "base64string"
        ///     }
        ///   ],
        ///   "technicianId": 3,
        ///   "lastComment": {
        ///     "commentId": 7,
        ///     "userId": 2,
        ///     "comment": "Se reprograma por falta de insumos",
        ///     "createdAt": "2025-07-14T15:00:00Z"
        ///   }
        /// }
        /// </remarks>
        /// <param name="order">Objeto OrderFullDetailDto con los datos de la orden a actualizar.</param>
        /// <response code="200">Orden actualizada exitosamente.</response>
        /// <response code="400">Datos de orden inválidos.</response>
        /// <response code="500">Error interno al actualizar la orden.</response>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutOrder([FromBody] OrderDto order)
        {
            if (order == null || order.OrderId == null || order.OrderId <= 0)
                return BadRequest("Datos de orden inválidos.");
            try
            {
                await _service.UpdateOrderAsync(order);
                return StatusCode(StatusCodes.Status200OK, "Orden actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar la orden: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista las órdenes pendientes por técnico.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de órdenes pendientes agrupadas por técnico.
        /// </remarks>
        /// <response code="200">Lista de órdenes pendientes encontrada.</response>
        [HttpGet("pending-by-technician")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TechnicianPendingOrdersDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingOrdersByTechnician()
        {
            var result = await _service.ListPendingOrdersByTechnicianAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene el detalle de una orden por su ID.
        /// </summary>
        /// <remarks>
        /// Devuelve el detalle completo de una orden específica.
        /// </remarks>
        /// <param name="orderId">ID de la orden.</param>
        /// <response code="200">Detalle de la orden encontrado.</response>
        /// <response code="404">Orden no encontrada.</response>
        [HttpGet("{orderId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderFullDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetailById(int orderId)
        {
            var result = await _service.GetOrderDetailByIdAsync(orderId);
            if (result == null)
                return NotFound("Orden no encontrada.");
            return Ok(result);
        }

        /// <summary>
        /// Obtiene el detalle de órdenes por documento de cliente.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de órdenes asociadas al documento del cliente.
        /// </remarks>
        /// <param name="identificationNumber">Número de documento del cliente.</param>
        /// <response code="200">Lista de órdenes encontrada.</response>
        [HttpGet("by-customer/{identificationNumber}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<OrderFullDetailDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderDetailByCustomerDocument(string identificationNumber)
        {
            var result = await _service.GetOrderDetailByCustomerDocumentAsync(identificationNumber);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene el conteo de tickets por estado.
        /// </summary>
        /// <remarks>
        /// Devuelve el conteo de tickets agrupados por estado para un año, mes y técnico opcional.
        /// </remarks>
        /// <param name="year">Año.</param>
        /// <param name="month">Mes.</param>
        /// <param name="technicianId">ID del técnico (opcional).</param>
        /// <response code="200">Conteo de tickets encontrado.</response>
        [HttpGet("ticket-count")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TicketCountByStatusDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketCountByStatus([FromQuery] int year, [FromQuery] int month, [FromQuery] int? technicianId)
        {
            var result = await _service.GetTicketCountByStatusAsync(year, month, technicianId);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene las soluciones de una orden.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de soluciones asociadas a una orden.
        /// </remarks>
        /// <param name="orderId">ID de la orden.</param>
        /// <response code="200">Lista de soluciones encontrada.</response>
        [HttpGet("{orderId:int}/solutions")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<SolutionDetailDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSolutionsByOrder(int orderId)
        {
            var result = await _service.GetSolutionsByOrderAsync(orderId);
            return Ok(result);
        }
    }
}