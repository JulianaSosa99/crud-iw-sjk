using login_api_iw_js.Models;
using System.ComponentModel.DataAnnotations;

public class Tema
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    public ICollection<Objetivo> Objetivos { get; set; }
    public ICollection<Hito> Hitos { get; set; }
 }
