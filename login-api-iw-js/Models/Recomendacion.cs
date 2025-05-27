namespace login_api_iw_js.Models
{
    public class Recomendacion
    {
        public int Id { get; set; }

        public int TemaId { get; set; }
        public Tema Tema { get; set; }

        public string NivelRiesgo { get; set; } // 'Bajo', 'Medio', 'Alto'
        public string Mensaje { get; set; }
    }

}
