using login_api_iw_js.LoginApi_Models;
using login_api_iw_js.Models;

public class UsuarioObjetivo
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public int ObjetivoId { get; set; }

    public Objetivo Objetivo { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public ICollection<Progreso> Progresos { get; set; }
}
