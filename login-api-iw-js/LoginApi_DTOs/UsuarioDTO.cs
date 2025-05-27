namespace login_api_iw_js.LoginApi_DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Contraseña encriptada

        public string Rol { get; set; }
    }
}
