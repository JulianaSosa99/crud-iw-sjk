using login_api_iw_js.LoginApi_Services;
using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.LoginApi_Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;


        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}
