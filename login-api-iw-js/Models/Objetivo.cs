namespace login_api_iw_js.Models
{
    public class Objetivo
    {
        public int ObjetivoID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioID { get; set; }
        public int TemaID { get; set; }
        public int? NivelEvaluacion { get; set; } // 1 a 5, puede ser null al inicio
    }
}
