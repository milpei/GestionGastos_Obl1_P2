using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class GerenteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
