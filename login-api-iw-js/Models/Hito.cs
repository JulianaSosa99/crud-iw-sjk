namespace login_api_iw_js.Models
{
    public class Hito
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public int ObjetivoId { get; set; }
        public Objetivo Objetivo { get; set; }

        public int TemaId { get; set; }
        public Tema Tema { get; set; }

        public ICollection<Progreso> Progresos { get; set; }
    }

}
