using login_api_iw_js.DTOs;
using login_api_iw_js.Models;

using login_api_iw_js.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using login_api_iw_js.Data;
using Microsoft.EntityFrameworkCore;

namespace login_api_iw_js.Services.Implementations
{
    public class TemaService : ITemaService
    {
        private readonly AppDbContext _context;
        public TemaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Tema tema)
        {
            _context.Tema.AddAsync(tema);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tema = await _context.Tema.FindAsync(id);
            if(tema != null)
            {
                _context.Tema.Remove(tema);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tema>> GetAllAsync()
        {
            return await _context.Tema.ToListAsync();
        }

        public async Task<Tema> GetByIdAsync(int id)
        {
           return await _context.Tema.FindAsync(id);
        }

        public async Task UpdateAsync(Tema tema)
        {
            _context.Tema.Update(tema);
            await _context.SaveChangesAsync();
        }



    }
}
