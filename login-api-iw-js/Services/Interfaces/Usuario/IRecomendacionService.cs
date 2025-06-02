namespace login_api_iw_js.Services.Interfaces.Usuario
{
    public interface IRecomendacionService
    {
        Task<List<string>> GenerarRecomendacionesAsync(int usuarioId);
    }
}
