using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandService service) : ControllerBase
    {
        private readonly IBrandService _service = service;

        /// <summary>
        /// Obtiene todas las marcas activas.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de todas las marcas que están activas en el sistema.
        /// </remarks>
        /// <response code="200">Lista de marcas activas encontrada.</response>
        /// <response code="404">No se encontraron marcas activas.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<BrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _service.GetActiveBrandsAsync();
            if (brands.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, brands);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No active brands found.");
            }
        }

        /// <summary>
        /// Agrega una nueva marca.
        /// </summary>
        /// <remarks>
        /// Crea una nueva marca en el sistema. El campo <c>BrandId</c> debe ser nulo.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "name": "Marca Ejemplo",
        ///   "status": true
        /// }
        /// </remarks>
        /// <param name="brand">Objeto BrandDto con los datos de la marca a agregar.</param>
        /// <response code="201">Marca agregada exitosamente.</response>
        /// <response code="400">Datos de marca inválidos.</response>
        /// <response code="500">Error interno al agregar la marca.</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostBrand([FromBody] BrandDto brand)
        {
            if (brand == null || brand.BrandId != null)
            {
                return BadRequest("Invalid brand data.");
            }
            try
            {
                await _service.AddBrandAsync(brand);
                return StatusCode(StatusCodes.Status201Created, $"Brand {brand.Name} added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding brand: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una marca existente.
        /// </summary>
        /// <remarks>
        /// Actualiza los datos de una marca existente. El campo <c>BrandId</c> debe ser mayor a 0.
        /// 
        /// <b>Ejemplo de body:</b>
        /// {
        ///   "brandId": 1,
        ///   "name": "Marca Actualizada",
        ///   "status": true
        /// }
        /// </remarks>
        /// <param name="brand">Objeto BrandDto con los datos de la marca a actualizar.</param>
        /// <response code="200">Marca actualizada exitosamente.</response>
        /// <response code="400">Datos de marca inválidos.</response>
        /// <response code="500">Error interno al actualizar la marca.</response>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutBrand([FromBody] BrandDto brand)
        {
            if (brand == null || brand.BrandId <= 0)
            {
                return BadRequest("Invalid brand data.");
            }
            try
            {
                await _service.UpdateBrandAsync(brand);
                return StatusCode(StatusCodes.Status200OK, $"Brand {brand.Name} updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating brand: {ex.Message}");
            }
        }
    }
}
