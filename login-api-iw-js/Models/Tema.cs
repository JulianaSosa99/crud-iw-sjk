using login_api_iw_js.Models;

public class Tema
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public ICollection<Objetivo> Objetivos { get; set; }
    public ICollection<Hito> Hitos { get; set; }
    public ICollection<Recomendacion> Recomendaciones { get; set; }
}
