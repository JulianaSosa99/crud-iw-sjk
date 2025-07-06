using login_api_iw_js.DTOs;

namespace login_api_iw_js.Validators.Interfaces
{
    /// Base para las reglas de validación de Hito.
    /// Permite aplicar el principio OCP permitiendo nuevas validaciones sin modificar código existente.
    public interface IHitoReglaValidacion
    {
        void Validar(HitoCreateDto dto);
    }
}
