using login_api_iw_js.DTOs;

namespace login_api_iw_js.Services.Interfaces
{
    public interface IHitoService
    {
        Task InsertarHitoAsync(HitoCreateDto dto);
        Task<List<HitoResponseDto>> ObtenerPorObjetivoAsync(int objetivoId);
        Task MarcarCumplidoAsync(HitoMarcarCumplidoDto dto);
    }
}
