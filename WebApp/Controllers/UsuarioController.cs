using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema s = Sistema.Instancia;
        public IActionResult MiPerfil()
        {
            Usuario u = s.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));

            ViewBag.gastoUsuarioXMesX = s.GastoUsuarioXMesX(u, DateTime.Now);

            return View(u);
            
        }


    }
}
