using login_api_iw_js.Models;

namespace login_api_iw_js.Repositories.Interfaces
{
    public interface IHitoRepository
    {
        Task<bool> ObjetivoExisteAsync(int objetivoId);
        Task CrearAsync(Hito hito);
    }
}
