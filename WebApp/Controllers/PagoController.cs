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



        public IActionResult CreateRecurrente()
        {
            List<TipoDeGasto> listaT = s.TiposDeGasto;
            return View(listaT);
        }

        [HttpPost]

        public IActionResult CreateRecurrente(MetodosDePago metodo, string tipo, string descripcion, decimal monto, DateTime fFin )
        {
            List<TipoDeGasto> listaT = s.TiposDeGasto;

            try 
            {
                Usuario u = s.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));
                TipoDeGasto t = s.ObtenerTipoGastoPorNombre(tipo);
                

                Pago p = new Recurrente(DateTime.Now, fFin, metodo, t, u, descripcion, monto);

                s.AgregarPago(p);
                ViewBag.Msg = "Transaccion Exitosa";
            } 
            catch(Exception Ex)
            {
                ViewBag.Msg = Ex.Message;
            }
            
                return View(listaT);
            
        }


        public IActionResult CreateUnico()
        {
            List<TipoDeGasto> listaT = s.TiposDeGasto;
            return View(listaT);
        }

        [HttpPost]

        public IActionResult CreateUnico(MetodosDePago metodo, string tipo, string descripcion, decimal monto, DateTime fPago, string numRecibo)
        {
            List<TipoDeGasto> listaT = s.TiposDeGasto;

            try
            {
                Usuario u = s.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));
                TipoDeGasto t = s.ObtenerTipoGastoPorNombre(tipo);


                Pago p = new Unico(fPago,numRecibo,metodo,t,u,descripcion,monto);

                s.AgregarPago(p);
                ViewBag.Msg = "Transaccion Exitosa";
            }
            catch (Exception Ex)
            {
                ViewBag.Msg = Ex.Message;
            }

            return View(listaT);

        }

    }
}
