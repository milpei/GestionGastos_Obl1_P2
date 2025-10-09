using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Unico : Pago
    {
        public DateTime FPago { get; set; }
        public string NumRecibo { get; set; }

        public decimal MontoFinal { get => CalcularMontoFinal(); }

        public Unico(DateTime fechaPago, string numeroRecibo, MetodosDePago metodoPago, TipoDeGasto tipoGasto, Usuario usuario, string descripcion, decimal monto) : base(metodoPago, tipoGasto, usuario, descripcion, monto)
        {
            FPago = fechaPago;
            NumRecibo = numeroRecibo;
        }

        public override string ToString()
        {
            return base.ToString() + $" Monto total: {MontoFinal}";
        }

       
        protected decimal Descuento()
        {
            if (MetodoPago == MetodosDePago.Efectivo) //si bien el enum guiarda el dato como un int, el dato enum no deja de ser enum... por eso MetodoPago == 2 da error.
            {
                return Monto * 0.20m;
            }
            else
            {
                return Monto * 0.10m;
            }
        }

        protected decimal CalcularMontoFinal()
        {
            return Monto - Descuento();
        }

        public override void Validar()
        {
            base.Validar();
            ValidarFPago();
            ValidarNumRec();
        }

        protected void ValidarFPago()
        {
            if (FPago == DateTime.MinValue) throw new Exception("Se debe ingresar una fecha de pago");
        }
        protected void ValidarNumRec()
        {
            if (string.IsNullOrEmpty(NumRecibo)) throw new Exception("Se debe ingresar un numero de recibo");
        }

        public override bool EsDelMesX(DateTime fecha)
        {
            return FPago.Month == fecha.Month && FPago.Year == fecha.Year; 

            
        }



    }
}
