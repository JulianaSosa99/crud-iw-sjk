using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace login_api_iw_js.LoginApi_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        // Constructor: inyectamos el servicio
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuario
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();  // Método para listar usuarios
            return Ok(usuarios);
        }

        // POST: api/usuario/registro
        [HttpPost("registro")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Registro([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                await _usuarioService.RegistroAsync(usuarioDTO, usuarioDTO.PasswordHash);
                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/usuario/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                await _usuarioService.ActualizarUsuarioAsync(id, usuarioDTO);
                return Ok("Usuario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/usuario/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                await _usuarioService.EliminarUsuarioAsync(id);
                return Ok("Usuario eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
