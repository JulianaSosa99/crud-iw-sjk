using login_api_iw_js.DTOs;
using login_api_iw_js.Validators.Interfaces;

namespace login_api_iw_js.Validators.Reglas
{
    public class ValidarSubtemasMinimos : IHitoReglaValidacion
    {
        public void Validar(HitoCreateDto dto)
        {
            if (dto.Subtemas == null || !dto.Subtemas.Any())
                throw new Exception("Debe incluir al menos un subtema.");
        }
    }
}
