using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces.Usuario
{
    public interface IProgresoService
    {
        Task RegistrarProgresoAsync(int usuarioId, ProgresoDto dto);
        Task<List<ProgresoDto>> ObtenerProgresosPorUsuario(int usuarioId);
    }
}
