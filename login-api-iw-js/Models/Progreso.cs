namespace login_api_iw_js.Models
{
    public class Progreso
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; } // Viene del token o login
        public int HitoId { get; set; }
        public Hito Hito { get; set; }

        public string Escala { get; set; } // 'Excelente', 'Bueno', 'Regular', 'Bajo', 'Sin avanzar'
    }

}
