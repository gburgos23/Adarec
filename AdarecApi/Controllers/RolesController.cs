using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService service) : ControllerBase
    {
        private readonly IRoleService _service = service;

        /// <summary>
        /// Obtiene todos los roles.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de todos los roles registrados en el sistema.
        /// 
        /// <b>Ejemplo de respuesta:</b>
        /// [
        ///   {
        ///     "rolId": 1,
        ///     "name": "Administrador",
        ///     "status": true
        ///   },
        ///   {
        ///     "rolId": 2,
        ///     "name": "Técnico",
        ///     "status": true
        ///   }
        /// ]
        /// </remarks>
        /// <response code="200">Lista de roles encontrada.</response>
        /// <response code="404">No se encontraron roles.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<RolDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _service.GetAllRolesAsync();
            if (roles.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, roles);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No roles found.");
            }
        }

        /// <summary>
        /// Agrega un nuevo rol.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo rol en el sistema. El campo <c>rolId</c> debe ser nulo.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "name": "Supervisor",
        ///   "status": true
        /// }
        /// </remarks>
        /// <param name="role">Objeto RolDto con los datos del rol a agregar.</param>
        /// <response code="201">Rol agregado exitosamente.</response>
        /// <response code="400">Datos de rol inválidos.</response>
        /// <response code="500">Error interno al agregar el rol.</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostRole([FromBody] RolDto role)
        {
            if (role == null)
            {
                return BadRequest("Invalid role data.");
            }
            try
            {
                await _service.AddRoleAsync(role);
                return StatusCode(StatusCodes.Status201Created, $"Role {role.Name} added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding role: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un rol existente.
        /// </summary>
        /// <remarks>
        /// Actualiza los datos de un rol existente. El campo <c>rolId</c> debe ser mayor a 0.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "rolId": 2,
        ///   "name": "Técnico Senior",
        ///   "status": true
        /// }
        /// </remarks>
        /// <param name="role">Objeto RolDto con los datos del rol a actualizar.</param>
        /// <response code="200">Rol actualizado exitosamente.</response>
        /// <response code="400">Datos de rol inválidos.</response>
        /// <response code="500">Error interno al actualizar el rol.</response>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutRole([FromBody] RolDto role)
        {
            if (role == null || role.RolId <= 0)
            {
                return BadRequest("Invalid role data.");
            }
            try
            {
                await _service.UpdateRoleAsync(role);
                return StatusCode(StatusCodes.Status200OK, $"Role {role.Name} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating role: {ex.Message}");
            }
        }
    }
}