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
    }
}