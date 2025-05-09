using login_api_iw_js.DTOs;
using login_api_iw_js.Models;
using login_api_iw_js.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace login_api_iw_js.Services.Implementations
{
    public class TemaService : ITemaService
    {
        private readonly SqlConnection _connection;
        private readonly IMapper _mapper;

        public TemaService(IDbConnection connection, IMapper mapper)
        {
            _connection = (SqlConnection)connection;
            _mapper = mapper;
        }

        public async Task<List<TemaResponseDto>> ObtenerTemasAsync()
        {
            var temas = new List<Tema>();

            using (var command = new SqlCommand("sp_ObtenerTemas", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        temas.Add(new Tema
                        {
                            TemaID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.GetString(2)
                        });
                    }
                }
            }

            return _mapper.Map<List<TemaResponseDto>>(temas);
        }

        public async Task InsertarTemaAsync(TemaCreateDto dto)
        {
            // Validaciones en C#
            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length < 3)
                throw new Exception("El nombre debe tener al menos 3 caracteres.");

            if (dto.Nombre.Length > 100)
                throw new Exception("El nombre no puede superar los 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(dto.Descripcion) && dto.Descripcion.Length > 255)
                throw new Exception("La descripción no puede superar los 255 caracteres.");

            using (var command = new SqlCommand("sp_InsertarTema", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", dto.Nombre);
                command.Parameters.AddWithValue("@Descripcion", dto.Descripcion ?? "");

                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> ExisteTemaAsync(string nombre)
        {
            using (var command = new SqlCommand("sp_ExisteTema", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", nombre);

                if (_connection.State != ConnectionState.Open)
                    await _connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();
                return Convert.ToBoolean(result);
            }
        }
        public async Task<TemaResponseDto> ObtenerPorIdAsync(int id)
        {
            using var command = new SqlCommand("sp_ObtenerTemaPorId", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TemaID", id);

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var tema = new Tema
                {
                    TemaID = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2)
                };
                return _mapper.Map<TemaResponseDto>(tema);
            }

            throw new Exception("Tema no encontrado.");
        }

        public async Task EliminarTemaAsync(int id)
        {
            using var command = new SqlCommand("sp_EliminarTema", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TemaID", id);

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }
        public async Task ActualizarTemaAsync(int id, TemaCreateDto dto)
        {
            using var command = new SqlCommand("sp_ActualizarTema", _connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@TemaID", id);
            command.Parameters.AddWithValue("@Nombre", dto.Nombre);
            command.Parameters.AddWithValue("@Descripcion", dto.Descripcion ?? "");

            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }

    }
}
