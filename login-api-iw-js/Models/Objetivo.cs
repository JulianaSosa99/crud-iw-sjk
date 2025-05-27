namespace login_api_iw_js.Models
{
    public class Objetivo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int TemaId { get; set; }
        public Tema Tema { get; set; }

        public ICollection<Hito> Hitos { get; set; }
    }

}
