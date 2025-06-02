using AutoMapper;
using login_api_iw_js.Data;
using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Administrador
{
    public class ObjetivoService : IObjetivoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ObjetivoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ObjetivoResponseDto>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var objetivos = await _context.UsuarioObjetivo
                .Where(uo => uo.UsuarioId == usuarioId)
                .Include(uo => uo.Objetivo)
                .Select(uo => new ObjetivoResponseDto
                {
                    Id = uo.ObjetivoId,
                    Nombre = uo.Objetivo.Nombre,
                    FechaAsignacion = uo.FechaAsignacion
                }).ToListAsync();

            return objetivos;
        }

        public async Task<int> CrearObjetivoAsync(ObjetivoCreateDto dto, int usuarioId)
        {
            var objetivo = new Objetivo
            {
                Nombre = dto.NombreObjetivo,
                Temas = new List<Tema> { await _context.Tema.FindAsync(dto.TemaId) }
            };
            _context.Objetivo.Add(objetivo);
            await _context.SaveChangesAsync();

            // Asignar al usuario que lo crea (si aplica)
            _context.UsuarioObjetivo.Add(new UsuarioObjetivo
            {
                UsuarioId = usuarioId,
                ObjetivoId = objetivo.Id,
                FechaAsignacion = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

           
            await AsignarNuevoObjetivoATodosLosUsuariosDelTema(dto.TemaId, objetivo.Id);

            return objetivo.Id;
        }
        public async Task ActualizarObjetivoAsync(int objetivoId, ObjetivoUpdateDto dto)
        {
            var objetivo = await _context.Objetivo.FindAsync(objetivoId);
            if (objetivo == null) throw new Exception("Objetivo no encontrado");

            objetivo.Nombre = dto.NombreObjetivo;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarObjetivoAsync(int objetivoId)
        {
            var objetivo = await _context.Objetivo
                .Include(o => o.Hitos)
                .FirstOrDefaultAsync(o => o.Id == objetivoId);

            if (objetivo == null) return;

            _context.Objetivo.Remove(objetivo);
            await _context.SaveChangesAsync();
        }
        public async Task AsignarNuevoObjetivoATodosLosUsuariosDelTema(int temaId, int nuevoObjetivoId)
        {
            var usuariosConTema = await _context.UsuarioObjetivo
     .Where(uo => uo.Objetivo.Temas.Any(t => t.Id == temaId))
     .Select(uo => uo.UsuarioId)
     .Distinct()
     .ToListAsync();

            foreach (var usuarioId in usuariosConTema)
            {
                var yaAsignado = await _context.UsuarioObjetivo
                    .AnyAsync(uo => uo.UsuarioId == usuarioId && uo.ObjetivoId == nuevoObjetivoId);

                if (!yaAsignado)
                {
                    _context.UsuarioObjetivo.Add(new UsuarioObjetivo
                    {
                        UsuarioId = usuarioId,
                        ObjetivoId = nuevoObjetivoId,
                        FechaAsignacion = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
