using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces
{
    public interface ITemaService
    {
        Task<List<TemaResponseDto>> ObtenerTemasAsync();
        Task InsertarTemaAsync(TemaCreateDto dto);
        Task<bool> ExisteTemaAsync(string nombre);
        Task ActualizarTemaAsync(int id, TemaCreateDto dto);
        Task EliminarTemaAsync(int id);

    }
}
