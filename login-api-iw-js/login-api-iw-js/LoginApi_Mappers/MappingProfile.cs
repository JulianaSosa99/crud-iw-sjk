using AutoMapper;
using login_api_iw_js.LoginApi_DTOs;
using login_api_iw_js.LoginApi_Models;

namespace login_api_iw_js.LoginApi_Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario,UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, LoginDTO>();
        }
    }
}
