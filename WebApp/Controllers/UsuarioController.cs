using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema s = Sistema.Instancia;
        public IActionResult MiPerfil(string email)
        {
            Usuario u = s.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));
          

            return View();
        }
    }
}
