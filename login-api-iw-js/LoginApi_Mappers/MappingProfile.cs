using AutoMapper;
using login_api_iw_js.DTOs;
using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Models;
using login_api_iw_js.Models;

namespace login_api_iw_js.LoginApi_Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario,UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, LoginDTO>();
            CreateMap<Tema, TemaResponseDto>();
            CreateMap<TemaCreateDto, Tema>();
            CreateMap<Objetivo, ObjetivoResponseDto>();
            CreateMap<ObjetivoCreateDto, Objetivo>();
            CreateMap<Hito, HitoResponseDto>();
            CreateMap<HitoCreateDto, Hito>();
            CreateMap<HitoMarcarCumplidoDto, Hito>();
        }
    }
}
