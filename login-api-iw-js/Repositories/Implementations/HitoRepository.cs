using login_api_iw_js.Data;
using login_api_iw_js.Models;
using login_api_iw_js.Repositories.Interfaces;
using login_api_iw_js.Services.Interfaces.Administrador;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


namespace login_api_iw_js.Repositories.Implementations
{
    public class HitoRepository : IHitoRepository
    {
        private readonly AppDbContext _context;

        public HitoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ObjetivoExisteAsync(int objetivoId)
        {
            return await _context.Objetivo.AnyAsync(o => o.Id == objetivoId);
        }

        public async Task CrearAsync(Hito hito)
        {
            await _context.Hito.AddAsync(hito);
            await _context.SaveChangesAsync();
        }
    }
}
