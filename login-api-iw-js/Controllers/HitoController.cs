using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HitoController : ControllerBase
    {
        private readonly IHitoService _hitoService;

        public HitoController(IHitoService hitoService)
        {
            _hitoService = hitoService;
        }

        /// <summary>
        /// Crea un nuevo hito dentro de un objetivo
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HitoCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            try
            {
                await _hitoService.InsertarHitoAsync(dto);
                return Ok("Hito creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar hito: {ex.Message}");
            }
        }

        /// <summary>
        /// Lista los hitos de un objetivo
        /// </summary>
        [HttpGet("{objetivoId}")]
        public async Task<IActionResult> Get(int objetivoId)
        {
            try
            {
                var hitos = await _hitoService.ObtenerPorObjetivoAsync(objetivoId);
                return Ok(hitos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener hitos: {ex.Message}");
            }
        }

        /// <summary>
        /// Marca un hito como cumplido o no cumplido
        /// </summary>
        [HttpPut("marcar")]
        public async Task<IActionResult> Put([FromBody] HitoMarcarCumplidoDto dto)
        {
            try
            {
                await _hitoService.MarcarCumplidoAsync(dto);
                return Ok("Estado del hito actualizado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar hito: {ex.Message}");
            }
        }
    }
}
