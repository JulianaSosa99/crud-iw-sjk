using login_api_iw_js.Data;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Administrador
{
    public class AsignacionService : IAsigancionService

    {
        private readonly AppDbContext  _context; 
        public AsignacionService(AppDbContext context)
        {
            _context = context ;
        }

        //Metodo para asignar temas e hitos a los usuarios.
        public async Task AsignarTemasYHitosAUsuariosAsync(int usuarioId, List<int> temaids)
        {
            foreach(var temaid in temaids)
            {
                if(await ExisteAsignacionAsync(usuarioId, temaid)) continue; 
                var tema = await _context.Tema.Include(t => t.Hitos)
                    .FirstOrDefaultAsync(t => t.Id == temaid);

                if (tema == null) continue;
                foreach (var hito in tema.Hitos)
                {
                    var asignacion = new UsuarioObjetivo
                    {
                        UsuarioId = usuarioId,
                        ObjetivoId = hito.ObjetivoId, // aca tomo en cuenta que los hitos tienen un objetivo
                        FechaAsignacion = DateTime.UtcNow,
                       
                    };
                    _context.UsuarioObjetivo.Add(asignacion);
                  

                }

            }
        await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsignacionAsync(int usuarioId, int temaId)
        {
            return await _context.UsuarioObjetivo
                .Include(uo => uo.Objetivo)
                .ThenInclude(obj => obj.Temas).
                AnyAsync( uo=>uo.UsuarioId==usuarioId && uo.Objetivo.Temas.Any(t => t.Id==temaId));
        }
    }
}
