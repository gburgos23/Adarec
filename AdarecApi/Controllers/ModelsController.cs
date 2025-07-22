using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController(IModelService service) : ControllerBase
    {
        private readonly IModelService _service = service;

        /// <summary>
        /// Obtiene todos los modelos activos.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de todos los modelos que están activos en el sistema.
        /// </remarks>
        /// <response code="200">Lista de modelos activos encontrada.</response>
        /// <response code="404">No se encontraron modelos activos.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ModelDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetModels()
        {
            var models = await _service.GetActiveModelsAsync();
            if (models.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, models);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No active models found.");
            }
        }
    }
}