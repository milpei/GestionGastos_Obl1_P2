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
        public IActionResult index(string email, string pwd)
        {
            try
            {
                Usuario u = s.Login(email, pwd);
                //Chequeo en s.Login() que no sea null

                if (u.Cargo == Cargo.Empleado)
                {
                    HttpContext.Session.SetString("cargo", "empleado");
                    //no sera mejor guardar el mail? asi guardo los datos de quien ingresó ("email",u.Email)
                    return RedirectToAction("index","Empleado");
                }
                else
                {
                    HttpContext.Session.SetString("cargo", "gerente");
                    return RedirectToAction("index", "Gerente");
                }
                
                

            } catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
                //habria que redireccionarlo al login
            }
        }

    }
}
