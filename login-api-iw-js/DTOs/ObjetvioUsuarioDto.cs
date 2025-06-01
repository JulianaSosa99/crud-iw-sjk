namespace login_api_iw_js.DTOs
{
    public class ObjetivoUsuarioDto
    {
        public int Id { get; set; }
        public string NombreObjetivo { get; set; }
        public string TemaNombre { get; set; }
        public List<HitoUsuarioDto> Hitos { get; set; }
    }

    
}
