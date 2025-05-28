using login_api_iw_js.Models;

public class UsuarioObjetivo
{
    public int UsuarioId { get; set; }
    public int ObjetivoId { get; set; }
    public DateTime FechaAsignacion { get; set; }

    public ICollection<Progreso> Progresos { get; set; }
}
