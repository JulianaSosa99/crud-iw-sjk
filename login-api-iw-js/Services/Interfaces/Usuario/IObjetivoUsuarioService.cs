using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces.Usuario
{
    public interface IObjetivoUsuarioService
    {
        Task<List<ObjetivoUsuarioDto>> ObtenerObjetivosConHitosYTemasPorUsuario(int usuarioId);
    }
}
