using AutoMapper;
using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Helpers;
using login_api_iw_js.LoginApi_Models;
using login_api_iw_js.LoginApi_Repositories;

namespace login_api_iw_js.LoginApi_Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository; 

        }
        public async Task<UsuarioDTO> LoginUsuarios(string email, string password)
        {
            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);
            if (usuario == null|| !PasswordHelper.verifyPassword(password,usuario.PasswordHash))
            {
              return null;
            }
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> ObtenerPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task RegistroAsync(UsuarioDTO usuarioDTO, string password)
        {
           var usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.PasswordHash = PasswordHelper.HashPassword(password);
            await _usuarioRepository.CrearUsuarioAsync(usuario);

        }
    }
}
