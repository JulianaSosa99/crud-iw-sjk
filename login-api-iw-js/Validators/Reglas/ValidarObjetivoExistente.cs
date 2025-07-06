using login_api_iw_js.Data;
using login_api_iw_js.DTOs;
using login_api_iw_js.Validators.Interfaces;

namespace login_api_iw_js.Validators.Reglas
{
    /// Regla de validación que verifica si el ObjetivoId existe en base de datos.
    public class ValidarObjetivoExistente : IHitoReglaValidacion

    {
        private readonly AppDbContext _context;

        public ValidarObjetivoExistente(AppDbContext context)
        {
            _context = context;
        }

        public void Validar(HitoCreateDto dto)
        {
            var existe = _context.Objetivo.Any(o => o.Id == dto.ObjetivoId);
            if (!existe)
                throw new Exception("El ObjetivoId no existe.");
        }
        
    }
}
