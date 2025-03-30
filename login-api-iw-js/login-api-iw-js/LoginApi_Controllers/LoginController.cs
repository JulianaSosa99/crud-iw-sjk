using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace login_api_iw_js.LoginApi_Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration; // Inyectamos IConfiguration

        public LoginController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Primero validamos las credenciales
            var usuario = await _usuarioService.LoginUsuarios(loginDTO.Email, loginDTO.Password);

            if (usuario == null)
            {
                return BadRequest("Credenciales incorrectas");
            }

            // Aquí validamos si el usuario es admin (suponiendo que tienes un campo Role)
            if (usuario.Email == "Admin" || usuario.PasswordHash == "mB$5Z*4o3")
            {
                // Si es admin, generamos el JWT con el rol de "admin"
                var token = GenerateJwtToken(usuario.Email, "admin", usuario.Id);
                return Ok(new { Token = token });
            }

            // Si no es admin, retornamos un token estándar con su rol
            var userRole = usuario.Rol; // Asumiendo que tienes un campo Role en el usuario
            var userToken = GenerateJwtToken(usuario.Email, userRole, usuario.Id);

            return Ok(new { Token = userToken });
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

        private string GenerateJwtToken(string email, string role, int userId)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, email),
        new Claim(ClaimTypes.Role, role),
        new Claim("id", userId.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
