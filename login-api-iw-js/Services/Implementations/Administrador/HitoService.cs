using AutoMapper;
using login_api_iw_js.Data;
using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Administrador
{
    public class HitoService : IHitoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HitoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HitoResponseDto>> ObtenerTodosAsync()
        {
            var hitos = await _context.Hito.Include(h => h.Subtemas).ToListAsync();
            return _mapper.Map<List<HitoResponseDto>>(hitos);
        }

        public async Task<HitoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var hito = await _context.Hito.Include(h => h.Subtemas).FirstOrDefaultAsync(h => h.Id == id);
            return hito == null ? null : _mapper.Map<HitoResponseDto>(hito);
        }

        public async Task CrearAsync(HitoCreateDto dto)
        {
            var existeObjetivo = await _context.Objetivo.AnyAsync(o => o.Id == dto.ObjetivoId);
            if (!existeObjetivo)
                throw new Exception("El ObjetivoId no existe.");

            var hito = new Hito
            {
                Descripcion = dto.Descripcion,
                Calificacion = dto.Calificacion,
                ObjetivoId = dto.ObjetivoId,
                TemaId = dto.TemaId,
                Subtemas = dto.Subtemas?.Select(s => new Subtema
                {
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    RecursoUrl = s.RecursoUrl
                }).ToList()
            };

            await _context.Hito.AddAsync(hito);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(int id, HitoUpdateDto dto)
        {
            var hito = await _context.Hito.Include(h => h.Subtemas).FirstOrDefaultAsync(h => h.Id == id);
            if (hito == null) return;

            hito.Descripcion = dto.Descripcion;
            hito.Calificacion = dto.Calificacion;

            hito.Subtemas.Clear();
            hito.Subtemas = dto.Subtemas?.Select(s => new Subtema
            {
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                RecursoUrl = s.RecursoUrl
            }).ToList();

            await _context.SaveChangesAsync();
        }
       
        public async Task EliminarAsync(int id)
        {
            var hito = await _context.Hito.FindAsync(id);
            if (hito == null) return;

            _context.Hito.Remove(hito);
            await _context.SaveChangesAsync();
        }
    }
}
