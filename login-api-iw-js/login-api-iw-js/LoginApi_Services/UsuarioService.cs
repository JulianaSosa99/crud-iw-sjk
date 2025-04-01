using AutoMapper;
using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Helpers;
using login_api_iw_js.LoginApi_Models;
using login_api_iw_js.LoginApi_Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.LoginApi_Services
{
    public class UsuarioService : IUsuarioService
    {
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    // Constructor: inyectamos el repositorio y el mapper
    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    // Listar todos los usuarios
    public async Task<List<UsuarioDTO>> ListarUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return _mapper.Map<List<UsuarioDTO>>(usuarios);
    }

    // Login de usuario
    public async Task<UsuarioDTO> LoginUsuarios(string email, string password)
    {
        var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);
        if (usuario == null || !PasswordHelper.verifyPassword(password, usuario.PasswordHash))
        {
            return null;
        }
            bool esContraseñaCorrecta = PasswordHelper.verifyPassword(password, usuario.PasswordHash);
            if (!esContraseñaCorrecta)
            {
                return null; // Contraseña incorrecta
            }
            return _mapper.Map<UsuarioDTO>(usuario);
    }

    // Registro de nuevo usuario
    public async Task RegistroAsync(UsuarioDTO usuarioDTO, string password)
    {
        var usuarioExistente = await _usuarioRepository.ObtenerPorEmailAsync(usuarioDTO.Email);
        if (usuarioExistente != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        var usuario = _mapper.Map<Usuario>(usuarioDTO);
        usuario.PasswordHash = PasswordHelper.HashPassword(password);

        await _usuarioRepository.CrearUsuarioAsync(usuario);
    }

    // Obtener usuario por ID
    public async Task<UsuarioDTO> ObtenerPorIdAsync(int id)
    {
        var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
        return _mapper.Map<UsuarioDTO>(usuario);
    }

    // Actualizar usuario
    public async Task ActualizarUsuarioAsync(int id, UsuarioDTO usuarioDTO)
    {
        var usuarioExistente = await _usuarioRepository.ObtenerPorIdAsync(id);
        if (usuarioExistente == null)
        {
            throw new Exception("El usuario no existe.");
        }


        var usuario = _mapper.Map<Usuario>(usuarioDTO);
        usuario.Id = id;

            if (!string.IsNullOrWhiteSpace(usuario.PasswordHash))
            {
                usuario.PasswordHash = PasswordHelper.HashPassword(usuario.PasswordHash);
            }
            else
            {
                // Mantener la contraseña actual si no se envió una nueva
                usuario.PasswordHash = usuarioExistente.PasswordHash;
            }
            await _usuarioRepository.ActualizarUsuarioAsync(usuario);
    }

    // Eliminar usuario
    public async Task EliminarUsuarioAsync(int id)
    {
        var usuarioExistente = await _usuarioRepository.ObtenerPorIdAsync(id);
        if (usuarioExistente == null)
        {
            throw new Exception("El usuario no existe.");
        }

        await _usuarioRepository.EliminarUsuarioAsync(id);
    }
}
}

