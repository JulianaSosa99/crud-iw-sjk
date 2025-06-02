using login_api_iw_js.DTOs;
 
using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Usuario
{
    [ApiController]
    [Route("api/usuario/objetivos")]
    [Authorize(Roles = "Usuario")]
    public class ObjetivoUsuarioController : ControllerBase
    {
        private readonly IObjetivoUsuarioService _objetivoUsuarioService;

        public ObjetivoUsuarioController(IObjetivoUsuarioService objetivoUsuarioService)
        {
            _objetivoUsuarioService = objetivoUsuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ObjetivoUsuarioDto>>> ObtenerObjetivosAsignados()
        {
            int usuarioId = int.Parse(User.FindFirst("id")?.Value ?? "0");
            var objetivos = await _objetivoUsuarioService.ObtenerObjetivosConHitosYTemasPorUsuario(usuarioId);
            return Ok(objetivos);
        }
    }
}
