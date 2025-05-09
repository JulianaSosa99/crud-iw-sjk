using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace login_api_iw_js.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ObjetivoController : ControllerBase
    {
        private readonly IObjetivoService _objetivoService;

        public ObjetivoController(IObjetivoService objetivoService)
        {
            _objetivoService = objetivoService;
        }

        // GET: api/objetivo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int usuarioId = ObtenerUsuarioIdDesdeToken();
                var objetivos = await _objetivoService.ObtenerPorUsuarioAsync(usuarioId);
                return Ok(objetivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener objetivos: {ex.Message}");
            }
        }

        // POST: api/objetivo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ObjetivoCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            try
            {
                int usuarioId = ObtenerUsuarioIdDesdeToken();
                await _objetivoService.InsertarObjetivoAsync(usuarioId, dto);
                return Ok("Objetivo creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar objetivo: {ex.Message}");
            }
        }

        // DELETE: api/objetivo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int usuarioId = ObtenerUsuarioIdDesdeToken();
                await _objetivoService.EliminarObjetivoAsync(id, usuarioId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar objetivo: {ex.Message}");
            }
        }

        // Extraer ID del usuario desde el token JWT
        private int ObtenerUsuarioIdDesdeToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "id");
            if (claim == null)
                throw new Exception("No se pudo identificar al usuario.");
            return int.Parse(claim.Value);
        }
        // PUT: api/objetivo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ObjetivoCreateDto dto)
        {
            try
            {
                int usuarioId = ObtenerUsuarioIdDesdeToken();
                await _objetivoService.ActualizarObjetivoAsync(id, usuarioId, dto);
                return Ok("Objetivo actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar objetivo: {ex.Message}");
            }
        }

    }
}
