using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces.Administrador
{
    public interface IObjetivoService
    {
        Task<List<ObjetivoResponseDto>> ObtenerPorUsuarioAsync(int usuarioId);
        Task<int> CrearObjetivoAsync(ObjetivoCreateDto dto, int usuarioId); // Cambiado: retorna el id
        Task ActualizarObjetivoAsync(int objetivoId, ObjetivoUpdateDto dto);
        Task EliminarObjetivoAsync(int objetivoId);
    }
}
