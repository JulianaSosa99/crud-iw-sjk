namespace login_api_iw_js.LoginApi_Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Contraseña encriptada
        public string Rol { get; set; } // "Admin" o "Usuario"
        public ICollection<UsuarioObjetivo> UsuarioObjetivos { get; set; }

    }
}
