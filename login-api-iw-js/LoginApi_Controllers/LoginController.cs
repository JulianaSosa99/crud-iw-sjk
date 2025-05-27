using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace login_api_iw_js.LoginApi_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var usuarioDTO = await _usuarioService.LoginUsuarios(loginDTO.Email, loginDTO.Password);

            if (usuarioDTO == null)
            {
                return BadRequest("Credenciales incorrectas");
            }

            // Generar token según el rol que tiene en la base de datos
            var token = GenerateJwtToken(usuarioDTO.Email, usuarioDTO.Rol, usuarioDTO.Id);

            return Ok(new { Token = token });
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
