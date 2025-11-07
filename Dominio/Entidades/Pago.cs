using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public abstract class Pago : IValidar
    {
        private static int s_UltId = 0;
        protected int _id;

        public  MetodosDePago MetodoPago { get; set;}
        public TipoDeGasto TipoGasto { get; set; }
        public Usuario Usuario { get; set; }
        public  string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int Id {  get { return _id; } }

        public Pago (MetodosDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario, string descripcion, decimal monto)
        {
            MetodoPago = metodoDePago;
            TipoGasto = tipoDeGasto;
            Usuario = usuario;
            Descripcion = descripcion;
            Monto = monto;
            _id = ++s_UltId;
        }


        public virtual void Validar() 
        {
            ValidarUsuario();
            ValidarDesc();
            ValidarMonto();
            ValidarTipo();
        }
        protected virtual void ValidarUsuario() 
        {
            if (Usuario == null) throw new Exception("Se debe asignar un usuario");
        }
        protected virtual void ValidarDesc() 
        {
            if (string.IsNullOrEmpty(Descripcion)) throw new Exception("Debe agregar una descripción");
        }
        protected virtual void ValidarMonto() 
        {
            if (Monto <= 0) throw new Exception("El monto debe ser mayor a cero");
        }
        protected virtual void ValidarTipo()
        {
            TipoGasto.Validar();
        }
        
        public abstract bool EsDelMesX(DateTime fecha);

        public override string ToString()
        {
            return $"id: {_id}, Metodo de pago: {MetodoPago},";
        }

       
    }
}
