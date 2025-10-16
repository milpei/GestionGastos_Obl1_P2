using System;
using System.Collections.Generic;
using System.Globalization;
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

            TimeSpan diferenciaEnDias = FFin - FInicio;

            int cuotasOMeses = diferenciaEnDias.Days / 30;

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

            //int cuotasPendientes = ((FFin.Year - DateTime.Today.Year) * 12) + FFin.Month - DateTime.Today.Month; // No incluye la cuota del mes actual, para incluirla +1
            TimeSpan diferenciaEnDias = FFin - DateTime.Now;

            int cuotasPendientes = diferenciaEnDias.Days / 30;

            if (cuotasPendientes < 0) cuotasPendientes = 0;

            return cuotasPendientes.ToString();
        }

        public override bool EsDelMesX(DateTime fecha)
        {
            bool iniciaAntes = FInicio.Year < fecha.Year || (FInicio.Year == fecha.Year && FInicio.Month <= fecha.Month);

            bool terminaDespues = (FFin == DateTime.MinValue) || (FFin.Year > fecha.Year) || (FFin.Year == fecha.Year && FFin.Month >= fecha.Month);

            return iniciaAntes && terminaDespues;

        }

        public override string ToString()
        {
            return base.ToString() + $" Monto total: {MontoFinal}, Pagos pendientes: {MostrarCuotasORecurrente()}";
        }

    }
}
