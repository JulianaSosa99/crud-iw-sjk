using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces.Administrador
{
    public interface IHitoService
    {
        Task<List<HitoResponseDto>> ObtenerTodosAsync();
        Task<HitoResponseDto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(HitoCreateDto dto);
        Task ActualizarAsync(int id, HitoUpdateDto dto);
        Task EliminarAsync(int id);
    }
}
