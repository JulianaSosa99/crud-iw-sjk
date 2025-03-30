using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Models;

namespace login_api_iw_js.LoginApi_Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> ListarUsuariosAsync();
        Task<UsuarioDTO> LoginUsuarios(string email, string password);
        Task RegistroAsync(UsuarioDTO usuarioDTO, string password);
        Task<UsuarioDTO> ObtenerPorIdAsync(int id);
        Task ActualizarUsuarioAsync(int id, UsuarioDTO usuarioDTO);
        Task EliminarUsuarioAsync(int id);

    }
}
