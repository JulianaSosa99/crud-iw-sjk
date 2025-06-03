using login_api_iw_js.Data;
using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Administrador
{
    public class ReporteHitoMayorMenorPuntaje
    {
        private readonly AppDbContext _context;

        public ReporteHitoMayorMenorPuntaje(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> ReporteMayorMenor()
        {
            var progresos = await _context.Progreso
               .Include(p => p.Hito)
               .ToListAsync();


            if (progresos == null || !progresos.Any())
                return new List<string> { "El usuario no tiene registros de progreso." };

            var mayor = progresos.OrderByDescending(p => p.ValorObtenido).First();
            var menor = progresos.OrderBy(p => p.ValorObtenido).First();

            return new List<string>
            {
                $"Mayor puntaje: Hito ID {mayor.Hito.Descripcion}, Objetivo ID {mayor.ObjetivoId} - Valor obtenido: {mayor.ValorObtenido}",
                $"Menor puntaje: Hito ID {menor.Hito.Descripcion}, Objetivo ID {menor.ObjetivoId} - Valor obtenido: {menor.ValorObtenido}"
            };
        }
        public async Task<List<string>> ReporteMenorUsuario()
        {
           
            var progresos = await _context.Progreso
              .Include(p => p.Hito)
              .Include(u=>u.UsuarioObjetivo.Usuario)
               .ToListAsync();

            var menor = progresos.OrderBy(p => p.ValorObtenido ).First();
            return new List<string>
            {

                $"Menor puntaje: Hito ID {menor.Hito.Descripcion}, Objetivo ID {menor.ObjetivoId} - Valor obtenido: {menor.ValorObtenido}, Usuario :{menor.UsuarioObjetivo.Usuario.Nombre}"
            };

        }
    }
}
