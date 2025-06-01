using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Administrador
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _temaService;

        public TemaController(ITemaService temaService)
        {
            _temaService = temaService;
        }

        // GET: api/tema
        [HttpGet]
        public async Task<ActionResult<List<TemaResponseDto>>> ObtenerTodos()
        {
            var temas = await _temaService.ObtenerTodosAsync();
            return Ok(temas);
        }

        // GET: api/tema/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemaResponseDto>> ObtenerPorId(int id)
        {
            var tema = await _temaService.ObtenerPorIdAsync(id);
            if (tema == null) return NotFound(new { mensaje = "Tema no encontrado" });
            return Ok(tema);
        }

        // POST: api/tema
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TemaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre del tema es obligatorio." });

            await _temaService.CrearAsync(dto);
            return Ok(new { mensaje = "Tema creado exitosamente" });
        }

        // PUT: api/tema/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] TemaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre del tema es obligatorio." });

            await _temaService.ActualizarAsync(id, dto);
            return Ok(new { mensaje = "Tema actualizado correctamente" });
        }

        // DELETE: api/tema/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _temaService.EliminarAsync(id);
            return Ok(new { mensaje = "Tema eliminado correctamente" });
        }
    }
}
