using System.ComponentModel.DataAnnotations;

namespace login_api_iw_js.Models
{
    public class Subtema
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public  string Nombre { get; set; } 
        public string? Descripcion { get; set; }
        [Url]
        public string? RecursoUrl { get; set; }
        [Required]
        public int HitoId { get; set; } 
        public Hito Hito { get; set; }
    }
}
