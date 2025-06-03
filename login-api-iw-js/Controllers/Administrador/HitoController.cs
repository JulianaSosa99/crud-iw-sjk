using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Implementations.Administrador;
using login_api_iw_js.Services.Implementations.Usuario;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Administrador
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class HitoController : ControllerBase
    {
        private readonly IHitoService _hitoService;
        private readonly ReporteHitoMayorMenorPuntaje _reporte1;

        public HitoController(IHitoService hitoService, ReporteHitoMayorMenorPuntaje reporte1)
        {
            _hitoService = hitoService;
            _reporte1 = reporte1;
            }

        // GET: api/hito
        [HttpGet]
        public async Task<ActionResult<List<HitoResponseDto>>> ObtenerTodos()
        {
            var hitos = await _hitoService.ObtenerTodosAsync();
            return Ok(hitos);
        }

        // GET: api/hito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HitoResponseDto>> ObtenerPorId(int id)
        {
            var hito = await _hitoService.ObtenerPorIdAsync(id);
            if (hito == null) return NotFound();
            return Ok(hito);
        }

        // POST: api/hito
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] HitoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _hitoService.CrearAsync(dto);
            return Ok(new { mensaje = "Hito creado correctamente" });
        }


        // PUT: api/hito/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] HitoUpdateDto dto)
        {
            await _hitoService.ActualizarAsync(id, dto);
            return Ok(new { mensaje = "Hito actualizado correctamente" });
        }

        // DELETE: api/hito/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _hitoService.EliminarAsync(id);
            return Ok(new { mensaje = "Hito eliminado correctamente" });
        }

        [HttpGet("reporte")]
        public async Task<ActionResult<List<string>>> ListadoRanking()
        {
            var resultado = await _reporte1.ReporteMayorMenor();
            var resultado2 = await _reporte1.ReporteMenorUsuario();
            return Ok(new
            {
                RankingMayorMenor = resultado,
                UsuarioMenorPuntaje = resultado2
            });
        }
        

    }
}
