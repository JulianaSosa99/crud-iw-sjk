namespace login_api_iw_js.DTOs
{
    public class HitoResponseDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int ObjetivoId { get; set; }
        public int TemaId { get; set; }
        public int? Calificacion { get; set; }
        public List<string>? Subtemas { get; set; }
    }
}
