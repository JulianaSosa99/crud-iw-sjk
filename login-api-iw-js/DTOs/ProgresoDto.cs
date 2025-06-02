namespace login_api_iw_js.DTOs
{
    public class ProgresoDto
    {
        public int ObjetivoId { get; set; }      // ID del objetivo al que pertenece el progreso
        public int HitoId { get; set; }          // ID del hito relacionado
        public string Escala { get; set; }       // Tipo de escala, por ejemplo: "5 estrellas"
        public int ValorObtenido { get; set; }   // Valor marcado por el usuario (ej. 3 estrellas)
    }
}
