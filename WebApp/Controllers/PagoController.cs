using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PagoController : Controller
    {
        Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PagosUsuXMesXDesc()
        {
            Usuario u = s.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));
            List<Pago> lista = s.PaUsXMeXDesc(u, DateTime.Now);

            return View("PagosUsuXMesXDesc", lista);
        }
    }
}
