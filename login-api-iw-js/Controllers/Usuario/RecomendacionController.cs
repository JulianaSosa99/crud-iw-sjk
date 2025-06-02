using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Usuario
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Usuario")]
    public class RecomendacionController : ControllerBase
    {
        private readonly IRecomendacionService _recomendacionService;

        public RecomendacionController(IRecomendacionService recomendacionService)
        {
            _recomendacionService = recomendacionService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var usuarioId = int.Parse(User.Claims.First(c => c.Type == "id").Value);
            var recomendaciones = await _recomendacionService.GenerarRecomendacionesAsync(usuarioId);

            if (recomendaciones.Count == 0)
                return Ok(new { mensaje = "¡Buen trabajo! No hay recomendaciones por ahora." });

            return Ok(new
            {
                mensaje = "Te recomendamos repasar estos temas para mejorar tu alcance en los hitos:",
                sugerencias = recomendaciones
            });
        }
    }
}
