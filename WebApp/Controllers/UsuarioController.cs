using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema s = Sistema.Instancia;
        public IActionResult PerfilEmpleado()
        {
            Usuario u = s.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));

            ViewBag.gastoUsuarioXMesX = s.GastoUsuarioXMesX(u, DateTime.Now);

            return View(u);
            
        }

        public IActionResult PerfilGerente()
        {
            Usuario u = s.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));

            ViewBag.gastoUsuarioXMesX = s.GastoUsuarioXMesX(u, DateTime.Now);

            List<Usuario> miembros = s.IntegrantesEquipo(u.Equipo.Nombre);
            miembros.Sort(new UsuarioMailOrdAsc());
            ViewBag.MiembrosDelEquipoX = miembros;

            return View(u);

            //PROXIMO PASO...HAY QUE AGREGAR UN FOREACH EN LA VIEWE Y QUE SE IMPORIMA EN LA WEB.
        }


    }
}
