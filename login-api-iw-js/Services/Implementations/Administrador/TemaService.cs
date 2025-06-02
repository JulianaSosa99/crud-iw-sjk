using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using login_api_iw_js.Data;
using Microsoft.EntityFrameworkCore;
using login_api_iw_js.Services.Interfaces.Administrador;

namespace login_api_iw_js.Services.Implementations.Administrador
{
    public class TemaService : ITemaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TemaResponseDto>> ObtenerTodosAsync()
        {
            var temas = await _context.Tema.ToListAsync();
            return _mapper.Map<List<TemaResponseDto>>(temas);
        }

        public async Task<TemaResponseDto> ObtenerPorIdAsync(int id)
        {
            var tema = await _context.Tema.FindAsync(id);
            return _mapper.Map<TemaResponseDto>(tema);
        }

        public async Task CrearAsync(TemaCreateDto dto)
        {
            var tema = _mapper.Map<Tema>(dto);
            _context.Tema.Add(tema);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(int id, TemaCreateDto dto)
        {
            var tema = await _context.Tema.FindAsync(id);
            if (tema == null) throw new Exception("Tema no encontrado");

            tema.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var tema = await _context.Tema.FindAsync(id);
            if (tema == null) throw new Exception("Tema no encontrado");

            _context.Tema.Remove(tema);
            await _context.SaveChangesAsync();
        }
    }

}
