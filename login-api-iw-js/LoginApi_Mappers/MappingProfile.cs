using AutoMapper;
using login_api_iw_js.DTOs;
using login_api_iw_js.DTOs.login_api_iw_js.DTOs;
using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Models;
using login_api_iw_js.Models;

namespace login_api_iw_js.LoginApi_Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, LoginDTO>();
            CreateMap<Tema, TemaResponseDto>();
            CreateMap<TemaCreateDto, Tema>();
            CreateMap<Subtema, SubtemaResponseDto>();
            CreateMap<SubtemaCreateDto, Subtema>();

            CreateMap<Objetivo, ObjetivoResponseDto>();
            CreateMap<ObjetivoCreateDto, Objetivo>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreObjetivo));
            CreateMap<ObjetivoUpdateDto, Objetivo>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreObjetivo));

            CreateMap<Hito, HitoResponseDto>()
                .ForMember(dest => dest.Subtemas, opt => opt.MapFrom(src => src.Subtemas.Select(s => s.Nombre)));
        }
    }
}
