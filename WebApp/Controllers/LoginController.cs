using Dominio;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        Sistema s = Sistema.Instancia;

       
       
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pwd)
        {
            try
            {
                Usuario u = s.Login(email, pwd);
                //Chequeo en s.Login() que no sea null

                    HttpContext.Session.SetString("cargo", u.Cargo.ToString());
                    HttpContext.Session.SetString("email", u.Email);

                if (HttpContext.Session.GetString("cargo") == "Empleado")
                {
                   return RedirectToAction("PerfilEmpleado", "Usuario");
                }
                else if(HttpContext.Session.GetString("cargo") == "Gerente")
                {
                    return RedirectToAction("PerfilGerente", "Usuario");
                }

                return View("Login");
                

            } catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View("Index");
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

    }
}
