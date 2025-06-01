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

        public async Task CrearObjetivoConHitosAsync(ObjetivoCreateDto dto, int usuarioId)
        {
            var objetivo = new Objetivo
            {
                Nombre = dto.NombreObjetivo,
                Hitos = dto.Hitos.Select(h => new Hito
                {
                    Descripcion = h.Descripcion,
                    Calificacion = h.Calificacion,
                    TemaId = h.TemaId,
                    Subtemas = h.Subtemas?.Select(s => new Subtema
                    {
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        RecursoUrl = s.RecursoUrl
                    }).ToList()
                }).ToList()
            };

            var tema = await _context.Tema.FindAsync(dto.TemaId);
            if (tema != null)
            {
                objetivo.Temas = new List<Tema> { tema };
            }

            await _context.Objetivo.AddAsync(objetivo);
            await _context.SaveChangesAsync();

            var relacion = new UsuarioObjetivo
            {
                UsuarioId = usuarioId,
                ObjetivoId = objetivo.Id,
                FechaAsignacion = DateTime.UtcNow
            };

            await _context.UsuarioObjetivo.AddAsync(relacion);
            await _context.SaveChangesAsync();
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
    }
}
