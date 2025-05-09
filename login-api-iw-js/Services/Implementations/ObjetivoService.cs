using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace login_api_iw_js.Services.Implementations
{
    public class ObjetivoService : IObjetivoService
    {
        private readonly SqlConnection _connection;
        private readonly IMapper _mapper;

        public ObjetivoService(IDbConnection connection, IMapper mapper)
        {
            _connection = (SqlConnection)connection;
            _mapper = mapper;
        }

        public async Task<List<ObjetivoResponseDto>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var objetivos = new List<Objetivo>();

            using (var command = new SqlCommand("sp_ListarObjetivosPorUsuario", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UsuarioID", usuarioId);

                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objetivos.Add(new Objetivo
                        {
                            ObjetivoID = reader.GetInt32(0),
                            Titulo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            TemaID = reader.GetInt32(3),
                            NivelEvaluacion = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                        });
                    }
                }
            }

            return _mapper.Map<List<ObjetivoResponseDto>>(objetivos);
        }
        public async Task EliminarObjetivoAsync(int objetivoId, int usuarioId)
        {
            using var command = new SqlCommand("sp_EliminarObjetivo", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ObjetivoID", objetivoId);
            command.Parameters.AddWithValue("@UsuarioID", usuarioId);

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }

        public async Task InsertarObjetivoAsync(int usuarioId, ObjetivoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo) || dto.Titulo.Length < 3)
                throw new Exception("El título debe tener al menos 3 caracteres.");

            if (dto.Titulo.Length > 100)
                throw new Exception("El título no puede superar los 100 caracteres.");

            using (var command = new SqlCommand("sp_InsertarObjetivo", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Titulo", dto.Titulo);
                command.Parameters.AddWithValue("@Descripcion", dto.Descripcion ?? "");
                command.Parameters.AddWithValue("@UsuarioID", usuarioId);
                command.Parameters.AddWithValue("@TemaID", dto.TemaID);

                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task ActualizarObjetivoAsync(int objetivoId, int usuarioId, ObjetivoCreateDto dto)
        {
            using var command = new SqlCommand("sp_ActualizarObjetivo", _connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ObjetivoID", objetivoId);
            command.Parameters.AddWithValue("@UsuarioID", usuarioId);
            command.Parameters.AddWithValue("@Titulo", dto.Titulo);
            command.Parameters.AddWithValue("@Descripcion", dto.Descripcion ?? "");
            command.Parameters.AddWithValue("@TemaID", dto.TemaID);

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }

    }
}
