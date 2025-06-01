using login_api_iw_js.Data;
using login_api_iw_js.DTOs;

using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Usuario
{
    public class ObjetivoUsuarioService : IObjetivoUsuarioService
    {
        private readonly AppDbContext _context;

        public ObjetivoUsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ObjetivoUsuarioDto>> ObtenerObjetivosConHitosYTemasPorUsuario(int usuarioId)
        {
            var objetivos = await _context.UsuarioObjetivo
                .Where(uo => uo.UsuarioId == usuarioId)
                .Include(uo => uo.Objetivo)
                    .ThenInclude(o => o.Hitos)
                .Include(uo => uo.Objetivo)
                    .ThenInclude(o => o.Temas)
                .Select(uo => new ObjetivoUsuarioDto
                {
                    Id = uo.Objetivo.Id,
                    NombreObjetivo = uo.Objetivo.Nombre,
                    TemaNombre = uo.Objetivo.Temas.FirstOrDefault().Nombre,
                    Hitos = uo.Objetivo.Hitos.Select(h => new HitoUsuarioDto
                    {
                        Id = h.Id,
                        Descripcion = h.Descripcion,
                        Calificacion = h.Calificacion
                    }).ToList()
                })
                .ToListAsync();

            return objetivos;
        }
    }
}
