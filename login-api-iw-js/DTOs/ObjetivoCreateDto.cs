using login_api_iw_js.DTOs;

public class ObjetivoCreateDto
{
    public string NombreObjetivo { get; set; }
    public int TemaId { get; set; }
    public List<HitoCreateDto> Hitos { get; set; }
}
