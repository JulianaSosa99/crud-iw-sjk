using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Administrador
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ObjetivoController : ControllerBase
    {
        private readonly IObjetivoService _objetivoService;

        public ObjetivoController(IObjetivoService objetivoService)
        {
            _objetivoService = objetivoService;
        }

        // GET: api/objetivo/por-usuario
        [HttpGet("por-usuario")]
        public async Task<ActionResult<List<ObjetivoResponseDto>>> ObtenerPorUsuario()
        {
            int usuarioId = int.Parse(User.FindFirst("id")?.Value ?? "0");
            var objetivos = await _objetivoService.ObtenerPorUsuarioAsync(usuarioId);
            return Ok(objetivos);
        }

        // POST: api/objetivo
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ObjetivoCreateDto dto)
        {
            int usuarioId = int.Parse(User.FindFirst("id")?.Value ?? "0");
            await _objetivoService.CrearObjetivoConHitosAsync(dto, usuarioId);
            return Ok(new { mensaje = "Objetivo creado correctamente" });
        }

        // PUT: api/objetivo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ObjetivoUpdateDto dto)
        {
            await _objetivoService.ActualizarObjetivoAsync(id, dto);
            return Ok(new { mensaje = "Objetivo actualizado correctamente" });
        }

        // DELETE: api/objetivo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _objetivoService.EliminarObjetivoAsync(id);
            return Ok(new { mensaje = "Objetivo eliminado correctamente" });
        }
    }
}
