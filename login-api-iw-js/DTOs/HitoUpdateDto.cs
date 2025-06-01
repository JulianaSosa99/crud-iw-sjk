namespace login_api_iw_js.DTOs
{
    public class HitoUpdateDto
    {
        public string Descripcion { get; set; }
        public int Calificacion { get; set; }
        public List<SubtemaCreateDto> Subtemas { get; set; }
    }
}
