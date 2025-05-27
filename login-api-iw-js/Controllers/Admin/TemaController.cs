using Microsoft.AspNetCore.Mvc;

namespace login_api_iw_js.Controllers.Admin
{
    public class TemaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
