using login_api_iw_js.Data;
using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Usuario
{
    public class ProgresoService : IProgresoService
    {
        private readonly AppDbContext _context;

        public ProgresoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarProgresoAsync(int usuarioId, ProgresoDto dto)
        {
            var hito = await _context.Hito.FindAsync(dto.HitoId);
            if (hito == null)
                throw new Exception("Hito no encontrado.");

            // Verificación opcional: valor no puede ser mayor al máximo definido por el admin
            if (hito.Calificacion.HasValue && dto.ValorObtenido > hito.Calificacion)
                throw new Exception("El valor obtenido excede la calificación máxima definida por el administrador.");

            var progreso = new Progreso
            {
                UsuarioId = usuarioId,
                ObjetivoId = dto.ObjetivoId,
                HitoId = dto.HitoId,
                Escala = dto.Escala,
                ValorObtenido = dto.ValorObtenido
            };

            _context.Progreso.Add(progreso);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ProgresoDto>> ObtenerProgresosPorUsuarioIdAdmin(int usuarioId)
        {
            return await _context.Progreso
                .Where(p => p.UsuarioId == usuarioId)
                .Select(p => new ProgresoDto
                {
                    ObjetivoId = p.ObjetivoId,
                    HitoId = p.HitoId,
                    Escala = p.Escala,
                    ValorObtenido = p.ValorObtenido
                }).ToListAsync();
        }

        public async Task<List<ProgresoDto>> ObtenerProgresosPorUsuario(int usuarioId)
        {
            return await _context.Progreso
                .Where(p => p.UsuarioId == usuarioId)
                .Select(p => new ProgresoDto
                {
                    ObjetivoId = p.ObjetivoId,
                    HitoId = p.HitoId,
                    Escala = p.Escala,
                    ValorObtenido = p.ValorObtenido
                }).ToListAsync();
        }
    }
}
