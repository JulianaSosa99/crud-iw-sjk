using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Implementations;
using login_api_iw_js.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _service;

        public TemaController(ITemaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos los temas disponibles.
        /// Solo accesible para usuarios autenticados.
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var temas = await _service.ObtenerTemasAsync();
                return Ok(temas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener temas: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea un nuevo tema.
        /// Solo accesible para administradores.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] TemaCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Los datos enviados no son válidos.");

            try
            {
                if (await _service.ExisteTemaAsync(dto.Nombre))
                    return BadRequest("Ya existe un tema con ese nombre.");

                await _service.InsertarTemaAsync(dto);
                return Ok("Tema creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar tema: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] TemaCreateDto dto)
        {
            try
            {
                await _service.ActualizarTemaAsync(id, dto);
                return Ok("Tema actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar tema: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.EliminarTemaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar tema: {ex.Message}");
            }
        }

    }
}
