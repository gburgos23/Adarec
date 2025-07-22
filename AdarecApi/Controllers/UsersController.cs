using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        /// <summary>
        /// Obtiene todos los usuarios activos.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de todos los usuarios activos en el sistema.
        /// </remarks>
        /// <response code="200">Lista de usuarios encontrada.</response>
        /// <response code="404">No se encontraron usuarios activos.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TechnicianDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetAllUsersAsync();
            if (users.Count > 0)
                return StatusCode(StatusCodes.Status200OK, users);
            else
                return StatusCode(StatusCodes.Status404NotFound, "No active users found.");
        }

        /// <summary>
        /// Agrega un nuevo usuario.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo usuario. El campo <c>UserId</c> debe ser nulo.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "name": "Usuario Ejemplo",
        ///   "email": "usuario@ejemplo.com",
        ///   "password": "123456",
        ///   "status": true
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUser([FromBody] TechnicianDto user)
        {
            if (user == null || user.TechnicianId != null)
                return BadRequest("Invalid user data.");
            try
            {
                await _service.AddUserAsync(user);
                return StatusCode(StatusCodes.Status201Created, $"User {user.Name} added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding user: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <remarks>
        /// Actualiza los datos de un usuario existente. El campo <c>UserId</c> debe ser mayor a 0.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "userId": 1,
        ///   "name": "Usuario Actualizado",
        ///   "email": "usuario@ejemplo.com",
        ///   "password": "123456",
        ///   "status": true
        /// }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUser([FromBody] TechnicianDto user)
        {
            if (user == null || user.TechnicianId <= 0)
                return BadRequest("Invalid user data.");
            try
            {
                await _service.UpdateUserAsync(user);
                return StatusCode(StatusCodes.Status204NoContent, $"User {user.Name} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la carga de trabajo de los técnicos.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista con la cantidad de órdenes asignadas a cada técnico.
        /// </remarks>
        /// <response code="200">Lista de cargas de trabajo encontrada.</response>
        /// <response code="404">No se encontraron técnicos con carga de trabajo.</response>
        [HttpGet("workload")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TechnicianWorkloadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTechnicianWorkload()
        {
            var workload = await _service.GetTechnicianWorkloadAsync();
            if (workload.Count > 0)
                return StatusCode(StatusCodes.Status200OK, workload);
            else
                return StatusCode(StatusCodes.Status404NotFound, "No technician workload found.");
        }

        /// <summary>
        /// Elimina (inhabilita) un usuario por su ID.
        /// </summary>
        [HttpDelete("{userId:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _service.DeleteUserAsync(userId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}