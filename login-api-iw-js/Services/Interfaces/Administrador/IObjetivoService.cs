using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces.Administrador
{
    public interface IObjetivoService
    {
        Task<List<ObjetivoResponseDto>> ObtenerPorUsuarioAsync(int usuarioId);
        Task InsertarObjetivoAsync(int usuarioId, ObjetivoCreateDto dto);
        Task EliminarObjetivoAsync(int objetivoId, int usuarioId);
        Task ActualizarObjetivoAsync(int objetivoId, int usuarioId, ObjetivoCreateDto dto);

    }
}
