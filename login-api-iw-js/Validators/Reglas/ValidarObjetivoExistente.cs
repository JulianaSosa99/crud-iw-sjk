using login_api_iw_js.Data;
using login_api_iw_js.DTOs;
using login_api_iw_js.Repositories.Interfaces;
using login_api_iw_js.Validators.Interfaces;

namespace login_api_iw_js.Validators.Reglas
{
    /// Regla de validación que verifica si el ObjetivoId existe en base de datos.
    public class ValidarObjetivoExistente : IHitoReglaValidacion

    {
        //private readonly AppDbContext _context;

        private readonly IHitoRepository _repo;

        public ValidarObjetivoExistente(IHitoRepository repo)
        {
            _repo = repo;
        }

        public void Validar(HitoCreateDto dto)
        {
            var existe = _repo.ObjetivoExisteAsync(dto.ObjetivoId).Result;
            if (!existe)
                throw new Exception("El ObjetivoId no existe.");
        }

    }
}
