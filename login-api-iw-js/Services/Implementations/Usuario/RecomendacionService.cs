using login_api_iw_js.Data;
using login_api_iw_js.DTOs;
using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Usuario
{
    public class RecomendacionService : IRecomendacionService
    {
        private readonly AppDbContext _context;

        public RecomendacionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GenerarRecomendacionesAsync(int usuarioId)
        {
            var recomendaciones = new List<string>();

            // Traer todos los progresos del usuario, ordenados por fecha de creación (o ID si no hay fecha)
            var progresos = await _context.Progreso
                .Include(p => p.Hito)
                    .ThenInclude(h => h.Subtemas)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.Id) // Si tuvieras una columna FechaCreacion, úsala aquí
                .ToListAsync();

            // Agrupar por HitoId y tomar solo el último progreso por hito
            var ultimosProgresos = progresos
                .GroupBy(p => p.HitoId)
                .Select(g => g.First()) // El primero por orden descendente es el más reciente
                .ToList();

            foreach (var progreso in ultimosProgresos)
            {
                var califMax = progreso.Hito.Calificacion ?? 0;
                if (califMax > 0 && progreso.ValorObtenido <= califMax / 2)
                {
                    foreach (var subtema in progreso.Hito.Subtemas)
                    {
                        recomendaciones.Add($"Te recomendamos repasar el subtema: {subtema.Nombre} ({subtema.RecursoUrl ?? "sin recurso"})");
                    }
                }
            }

            return recomendaciones;
        }
    }
}
