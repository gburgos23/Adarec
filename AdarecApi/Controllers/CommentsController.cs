using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdarecApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(ICommentService service) : ControllerBase
    {
        private readonly ICommentService _service = service;

        /// <summary>
        /// Agrega un nuevo comentario a una orden.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo comentario para una orden existente. El campo <c>CommentId</c> debe ser nulo.
        ///
        /// <b>Ejemplo de body:</b>
        /// <code>
        /// {
        ///   "orderId": 25,
        ///   "comments": [
        ///     {
        ///       "userId": 2,
        ///       "comment": "El técnico llegó a tiempo y resolvió el problema.",
        ///       "createdAt": "2025-07-13T10:30:00Z"
        ///     }
        ///   ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="comment">Objeto OrderCommentsDto con los datos del comentario a agregar.</param>
        /// <response code="201">Comentario agregado exitosamente.</response>
        /// <response code="400">Datos de comentario inválidos.</response>
        /// <response code="500">Error interno al agregar el comentario.</response>
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostComment([FromBody] OrderCommentsDto comment)
        {
            if (comment == null || comment.OrderId == null)
                return BadRequest("Datos de comentario inválidos.");
            try
            {
                await _service.AddCommentAsync(comment);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    Message = "Comentario agregado exitosamente."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar el comentario: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un comentario existente de una orden.
        /// </summary>
        /// <remarks>
        /// Actualiza los datos de un comentario existente. El campo <c>CommentId</c> debe ser mayor a 0.
        ///
        /// <b>Ejemplo de body:</b>
        /// <code>
        /// {
        ///   "orderId": 25,
        ///   "comments": [
        ///     {
        ///       "commentId": 7,
        ///       "userId": 2,
        ///       "comment": "Se actualiza el comentario por información adicional.",
        ///       "createdAt": "2025-07-13T12:00:00Z"
        ///     }
        ///   ]
        /// }
        /// </code>
        /// </remarks>
        /// <param name="comment">Objeto OrderCommentsDto con los datos del comentario a actualizar.</param>
        /// <response code="200">Comentario actualizado exitosamente.</response>
        /// <response code="400">Datos de comentario inválidos.</response>
        /// <response code="500">Error interno al actualizar el comentario.</response>
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutComment([FromBody] OrderCommentsDto comment)
        {
            if (comment == null || comment.OrderId == null || comment.Comments == null || comment.Comments.Count == 0 || comment.Comments[0].CommentId <= 0)
                return BadRequest("Datos de comentario inválidos.");
            try
            {
                await _service.UpdateCommentAsync(comment);
                return StatusCode(StatusCodes.Status200OK, "Comentario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el comentario: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un comentario por su ID.
        /// </summary>
        /// <remarks>
        /// Elimina un comentario específico de una orden.
        /// </remarks>
        /// <param name="commentId">ID del comentario a eliminar.</param>
        /// <response code="200">Comentario eliminado exitosamente.</response>
        /// <response code="400">ID de comentario inválido.</response>
        /// <response code="500">Error interno al eliminar el comentario.</response>
        [HttpDelete("{commentId:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            if (commentId <= 0)
                return BadRequest("ID de comentario inválido.");
            try
            {
                await _service.DeleteCommentAsync(commentId);
                return StatusCode(StatusCodes.Status200OK, "Comentario eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el comentario: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista los comentarios agrupados por orden.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de comentarios agrupados por orden.
        /// </remarks>
        /// <response code="200">Lista de comentarios encontrada.</response>
        /// <response code="404">No se encontraron comentarios.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<OrderCommentsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentsByOrder()
        {
            var comments = await _service.ListCommentsByOrderAsync();
            if (comments.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, comments);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No se encontraron comentarios.");
            }
        }
    }
}