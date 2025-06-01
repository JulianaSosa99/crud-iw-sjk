using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;
using login_api_iw_js.Services.Interfaces.Administrador;
using login_api_iw_js.Data;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations.Administrador
{
    public class ObjetivoService : IObjetivoService

    {
        private readonly AppDbContext _context;
        public ObjetivoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task ActualizarObjetivoAsync(int objetivoId, int usuarioId, ObjetivoCreateDto dto)
        {
            var relacion = await _context.UsuarioObjetivo
               .Include(uo=>uo.Objetivo)
               .FirstOrDefaultAsync(uo=>uo.ObjetivoId== objetivoId && uo.UsuarioId==usuarioId);
            if(relacion!= null)
            {
                relacion.Objetivo.Nombre = dto.NombreObjetivo;
                await _context.SaveChangesAsync();
            }
        }

        public Task EliminarObjetivoAsync(int objetivoId, int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task InsertarObjetivoAsync(int usuarioId, ObjetivoCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<ObjetivoResponseDto>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }

}
