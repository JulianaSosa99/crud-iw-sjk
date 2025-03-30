using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Services;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.LoginApi_Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var usuario = await _usuarioService.LoginUsuarios(loginDTO.Email, loginDTO.Password);
            if (usuario == null)
            {
                return BadRequest("Credenciales Incorrectas");
            }
            if(usuario.Email=="Admin"|| usuario.PasswordHash=="mB$5Z*4o3")
            { 

            }
            return Ok(usuario);
        }
        [HttpPost("registro")]
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
    }
}
