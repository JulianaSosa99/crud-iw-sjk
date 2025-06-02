namespace login_api_iw_js.DTOs
{
    public class HitoCreateDto
    {
        public string Descripcion { get; set; }
        public int Calificacion { get; set; }
        public int ObjetivoId { get; set; }
        public int TemaId { get; set; }
        public List<SubtemaCreateDto> Subtemas { get; set; }
    }
}
