using Dominio;
using Dominio.Entidades;
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
                 
                return RedirectToAction("MiPerfil", "Usuario");
                
                

            } catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View("Index");
            }
        }

    }
}
