using login_api_iw_js.LoginApi_Models;

namespace login_api_iw_js.LoginApi_Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObtenerPorEmailAsync(string email);
        Task<Usuario> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task CrearUsuarioAsync(Usuario usuario);
        Task ActualizarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(int id);   

    }
}
