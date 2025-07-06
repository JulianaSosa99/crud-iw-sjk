using login_api_iw_js.DTOs;
using login_api_iw_js.Models;

namespace login_api_iw_js.Validators
{
    public class ProgresoValidator
    {
        public static void Validar(ProgresoDto dto, Hito? hito)
        {
            if (hito == null)
                throw new Exception("Hito no encontrado.");

            if (hito.Calificacion.HasValue && dto.ValorObtenido > hito.Calificacion)
                throw new Exception("El valor obtenido excede la calificación máxima definida por el administrador.");

            if (string.IsNullOrWhiteSpace(dto.Escala))
                throw new Exception("La escala no puede estar vacía.");

            if (dto.ValorObtenido < 0)
                throw new Exception("El valor obtenido debe ser mayor o igual a 0.");
        }
    }
}
