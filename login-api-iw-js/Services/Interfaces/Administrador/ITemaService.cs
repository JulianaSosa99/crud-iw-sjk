using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces.Administrador
{
    public interface ITemaService
    {
        Task<List<TemaResponseDto>> ObtenerTodosAsync();
        Task<TemaResponseDto> ObtenerPorIdAsync(int id);
        Task CrearAsync(TemaCreateDto dto);
        Task ActualizarAsync(int id, TemaCreateDto dto);
        Task EliminarAsync(int id);
    }
}
