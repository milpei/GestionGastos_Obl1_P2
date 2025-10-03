using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Recurrente : Pago
    {

        public DateTime FInicio { get; set; } 
        public DateTime FFin { get; set; } 
        public int NumCuotas { get { return ObtenerCuotas(); } }
        public decimal MontoFinal { get { return Monto * NumCuotas + Recargo() ; } }

        public Recurrente(DateTime fechaInicio, DateTime fechaFin, MetodosDePago metodoPago, TipoDeGasto tipoGasto, Usuario usuario, string descripcion, decimal monto) : base(metodoPago, tipoGasto,usuario, descripcion, monto)
        {
            FInicio = fechaInicio;
            FFin = fechaFin;
        }

        public override string ToString()
        {
            return base.ToString() + $"Monto total: {MontoFinal} Pagos pendientes: {MostrarCuotasORecurrente()}";
        }

        
        protected decimal Recargo() 
        {
            if (NumCuotas == 0 || NumCuotas <= 5)  
            {
                return Monto * 0.03m;
            } 
            else if (NumCuotas >= 6 && NumCuotas <= 9)
            {
                return Monto * 0.05m;
            }
            else
            {
                return Monto * 0.1m;
            }
        }

        protected int ObtenerCuotas()
        {
            if (FFin == DateTime.MinValue) return 0; // si da esto es porque no tiene fecha de fin y es recurrente.

            int cuotasOMeses = ((FFin.Year - FInicio.Year) * 12) + FFin.Month - FInicio.Month;

            return cuotasOMeses;
        }

        public override void Validar()
        {
            base.Validar();
            ValidarFInicio();
            ValidarFFin();
        }

        protected void ValidarFInicio()
        {
            if (FInicio == DateTime.MinValue) throw new Exception("Se debe ingresar una fecha de inicio");
        }

        protected void ValidarFFin()
        {
            if (FFin <= FInicio && FFin != DateTime.MinValue) throw new Exception("La fecha de fin no puede ser menor que la de inicio");
        }

        

        private string MostrarCuotasORecurrente() 
        {
            if (FFin == DateTime.MinValue) return "Recurrente";

            int cuotasPendientes = ((FFin.Year - DateTime.Today.Year) * 12) + FFin.Month - DateTime.Today.Month; // No incluye la cuota del mes actual, para incluirla +1

            return cuotasPendientes.ToString();
        }

        
    }
}
