using System.ComponentModel.DataAnnotations;

namespace login_api_iw_js.Models
{
    public class Objetivo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        public ICollection<Hito> Hitos { get; set; } 
        public ICollection<Tema> Temas { get; set; }
        public ICollection<UsuarioObjetivo> UsuarioObjetivos { get; set; }

    }

}
