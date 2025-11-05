using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
