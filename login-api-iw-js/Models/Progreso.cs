using login_api_iw_js.Models;
using System.ComponentModel.DataAnnotations;

public class Progreso
{
    public int Id { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int ObjetivoId { get; set; }

    [Required]
    public int HitoId { get; set; }

    [Required]
    [StringLength(20)]
    public string Escala { get; set; }

    public Hito Hito { get; set; }
    public UsuarioObjetivo UsuarioObjetivo { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int ValorObtenido { get; set; } // número de bolitas llenadas por el usuario

}
