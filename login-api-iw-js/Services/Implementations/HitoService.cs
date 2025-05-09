using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace login_api_iw_js.Services.Implementations
{
    public class HitoService : IHitoService
    {
        private readonly SqlConnection _connection;
        private readonly IMapper _mapper;

        public HitoService(IDbConnection connection, IMapper mapper)
        {
            _connection = (SqlConnection)connection;
            _mapper = mapper;
        }

        public async Task InsertarHitoAsync(HitoCreateDto dto)
        {
            using var command = new SqlCommand("sp_InsertarHito", _connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", dto.Nombre);
            command.Parameters.AddWithValue("@ObjetivoID", dto.ObjetivoID);

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<HitoResponseDto>> ObtenerPorObjetivoAsync(int objetivoId)
        {
            var hitos = new List<Hito>();

            using var command = new SqlCommand("sp_ObtenerHitosPorObjetivo", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ObjetivoID", objetivoId);

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                hitos.Add(new Hito
                {
                    HitoID = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Cumplido = reader.GetBoolean(2),
                    ObjetivoID = reader.GetInt32(3)
                });
            }

            return _mapper.Map<List<HitoResponseDto>>(hitos);
        }

        public async Task MarcarCumplidoAsync(HitoMarcarCumplidoDto dto)
        {
            using var command = new SqlCommand("sp_MarcarHitoCumplido", _connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@HitoID", dto.HitoID);
            command.Parameters.AddWithValue("@Cumplido", dto.Cumplido);

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }
    }
}
