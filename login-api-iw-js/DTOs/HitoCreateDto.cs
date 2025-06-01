namespace login_api_iw_js.DTOs
{
    public class HitoCreateDto
    {
        public string Descripcion { get; set; }
        public int Calificacion { get; set; }
        public List<string>? Subtemas { get; set; }
    }

}
