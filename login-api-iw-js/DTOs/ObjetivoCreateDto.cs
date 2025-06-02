using login_api_iw_js.DTOs;
using System.ComponentModel.DataAnnotations;

public class ObjetivoCreateDto
{
    [Required]
    [StringLength(150)]
    public string NombreObjetivo { get; set; }
    [Required]
    public int TemaId { get; set; }
 }