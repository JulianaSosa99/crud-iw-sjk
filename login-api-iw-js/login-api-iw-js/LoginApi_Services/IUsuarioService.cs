using login_api_iw_js.LoginApi_DTOs;

namespace login_api_iw_js.LoginApi_Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> LoginUsuarios(string email, string password);
        Task RegistroAsync(UsuarioDTO usuarioDTO,string password);
        Task<UsuarioDTO> ObtenerPorIdAsync(int id); 

}
}
