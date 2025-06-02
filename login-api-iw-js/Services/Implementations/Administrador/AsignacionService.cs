using login_api_iw_js.Data;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Administrador
{
    public class AsignacionService : IAsigancionService
    {
        private readonly AppDbContext _context;

        public AsignacionService(AppDbContext context)
        {
            _context = context;
        }

        // Método para asignar temas e hitos a los usuarios.
        public async Task AsignarTemasYHitosAUsuariosAsync(int usuarioId, List<int> temaIds)
        {
            var objetivosAsignados = new HashSet<int>();

            foreach (var temaId in temaIds)
            {
                if (await ExisteAsignacionAsync(usuarioId, temaId)) continue;

                var tema = await _context.Tema
                    .Include(t => t.Hitos)
                    .FirstOrDefaultAsync(t => t.Id == temaId);

                if (tema == null) continue;

                foreach (var hito in tema.Hitos)
                {
                    if (!objetivosAsignados.Contains(hito.ObjetivoId))
                    {
                        var yaAsignado = await _context.UsuarioObjetivo
                            .AnyAsync(uo => uo.UsuarioId == usuarioId && uo.ObjetivoId == hito.ObjetivoId);

                        if (!yaAsignado)
                        {
                            _context.UsuarioObjetivo.Add(new UsuarioObjetivo
                            {
                                UsuarioId = usuarioId,
                                ObjetivoId = hito.ObjetivoId,
                                FechaAsignacion = DateTime.UtcNow
                            });

                            objetivosAsignados.Add(hito.ObjetivoId);
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsignacionAsync(int usuarioId, int temaId)
        {
            return await _context.UsuarioObjetivo
                .Include(uo => uo.Objetivo)
                    .ThenInclude(obj => obj.Temas)
                .AnyAsync(uo => uo.UsuarioId == usuarioId && uo.Objetivo.Temas.Any(t => t.Id == temaId));
        }
    }
}
