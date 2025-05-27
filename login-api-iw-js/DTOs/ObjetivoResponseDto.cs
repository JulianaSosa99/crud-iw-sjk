namespace login_api_iw_js.DTOs
{
    public class ObjetivoResponseDto
    {
        public int ObjetivoID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int TemaID { get; set; }
        public int? NivelEvaluacion { get; set; }
    }
}
