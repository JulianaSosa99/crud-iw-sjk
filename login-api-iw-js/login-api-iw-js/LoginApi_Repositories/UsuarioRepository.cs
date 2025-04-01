using Dapper;
using login_api_iw_js.LoginApi_Models;
using System.Data;
using System.Data.Common;

namespace login_api_iw_js.LoginApi_Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;
        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;   
            
        }

        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("Id", usuario.Id);
            parametros.Add("Nombre", usuario.Nombre);
            parametros.Add("Email", usuario.Email);
            parametros.Add("PasswordHash", usuario.PasswordHash);
            parametros.Add("Rol", usuario.Rol);
            await _dbConnection.ExecuteAsync("sp_ActualizarUsuario", parametros, commandType: CommandType.StoredProcedure);

         
        }

        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("Nombre", usuario.Nombre);
            parametros.Add("Email", usuario.Email);
            parametros.Add("PasswordHash", usuario.PasswordHash);
            parametros.Add("Rol", usuario.Rol);
            await _dbConnection.ExecuteAsync("sp_CrearUsuario", parametros, commandType: CommandType.StoredProcedure);

        }

        public async Task EliminarUsuarioAsync(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("id", id);
            await _dbConnection.ExecuteAsync("sp_EliminarUsuario", parametros, commandType: CommandType.StoredProcedure);
            
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Usuario>("sp_obtenerUsuarios", commandType: CommandType.StoredProcedure);    
        }

        public async Task<Usuario> ObtenerPorEmailAsync(string email)
        {
            var parametros = new DynamicParameters();
            parametros.Add("Email", email);
            return await _dbConnection.QueryFirstOrDefaultAsync<Usuario>("sp_obtenerUsuariosPorEmail", parametros, commandType: CommandType.StoredProcedure);
           
        }
        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("Id", id);
            return await _dbConnection.QueryFirstOrDefaultAsync<Usuario>("sp_obtenerUsuariosPorId", parametros, commandType: CommandType.StoredProcedure);

        }
    }
}
