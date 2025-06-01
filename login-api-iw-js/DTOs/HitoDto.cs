using System.ComponentModel.DataAnnotations;

namespace login_api_iw_js.DTOs
{
    public class HitoDto
    {
        [Required]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del objetivo debe ser mayor a 0")]
        public int ObjetivoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tema debe ser mayor a 0")]
        public int TemaId { get; set; }

        [Range(1, 10, ErrorMessage = "La calificación debe estar entre 1 y 10")]
        public int? Calificacion { get; set; } // Puede empezar como null
    }
}
