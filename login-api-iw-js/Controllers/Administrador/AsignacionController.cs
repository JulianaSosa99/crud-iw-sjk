using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Administrador
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AsignacionController : ControllerBase
    {
        private readonly IAsigancionService _asignacionService;

        public AsignacionController(IAsigancionService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        // POST: api/asignacion/{usuarioId}
        [HttpPost("{usuarioId}")]
        public async Task<IActionResult> AsignarTemasYHitos(int usuarioId, [FromBody] List<int> temaIds)
        {
            if (temaIds == null || !temaIds.Any())
                return BadRequest(new { mensaje = "Debe proporcionar al menos un ID de tema." });

            await _asignacionService.AsignarTemasYHitosAUsuariosAsync(usuarioId, temaIds);
            return Ok(new { mensaje = "Asignación realizada correctamente." });
        }

        // GET: api/asignacion/existe?usuarioId=1&temaId=2
        [HttpGet("existe")]
        public async Task<IActionResult> VerificarAsignacion(int usuarioId, int temaId)
        {
            var existe = await _asignacionService.ExisteAsignacionAsync(usuarioId, temaId);
            return Ok(new { asignado = existe });
        }
    }
}
