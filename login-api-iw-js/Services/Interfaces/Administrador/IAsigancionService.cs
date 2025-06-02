namespace login_api_iw_js.Services.Interfaces.Administrador
{
    public interface IAsigancionService
    {
        Task AsignarTemasYHitosAUsuariosAsync(int usuarioId, List<int> temaids);
        Task<bool> ExisteAsignacionAsync(int usuarioId, int temaId);
    }
}
