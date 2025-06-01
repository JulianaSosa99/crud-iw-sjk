using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Usuario
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Usuario")]
    public class ProgresoController : ControllerBase
    {
        private readonly IProgresoService _progresoService;

        public ProgresoController(IProgresoService progresoService)
        {
            _progresoService = progresoService;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] ProgresoDto dto)
        {
            try
            {
                var usuarioId = int.Parse(User.Claims.First(c => c.Type == "id").Value);
                await _progresoService.RegistrarProgresoAsync(usuarioId, dto);
                return Ok(new { mensaje = "Progreso registrado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<ProgresoDto>>> Obtener()
        {
            var usuarioId = int.Parse(User.Claims.First(c => c.Type == "id").Value);
            var progresos = await _progresoService.ObtenerProgresosPorUsuario(usuarioId);
            return Ok(progresos);
        }
    }
}
