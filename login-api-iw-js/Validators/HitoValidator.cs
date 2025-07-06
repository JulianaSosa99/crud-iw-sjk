using login_api_iw_js.DTOs;
using login_api_iw_js.Validators.Interfaces;

namespace login_api_iw_js.Validators
{
    /// Orquestador que aplica una colección de reglas de validación para un HitoCreateDto.
    /// Permite centralizar y escalar las validaciones sin modificar el servicio que las usa.
    public class HitoValidator
    {
        private readonly List<IHitoReglaValidacion> _reglas;

        public HitoValidator(IEnumerable<IHitoReglaValidacion> reglas)
        {
            _reglas = reglas.ToList();
        }

        public void ValidarTodo(HitoCreateDto dto)
        {
            foreach (var regla in _reglas)
            {
                regla.Validar(dto);
            }
        }
    }
}
