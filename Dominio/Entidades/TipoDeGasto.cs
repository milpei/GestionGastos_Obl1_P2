using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class TipoDeGasto
    {
        // Esta va a ser la unica clase que lo hago sin abreviar. 
        private string _nombre;
        private string _descripcion;
        public string Nombre 
        { 
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public TipoDeGasto(string nom, string desc)
        {
            _nombre = nom;
            _descripcion = desc;
        }

        //Dicen que es buena practica tener un constructor vacío.

        public TipoDeGasto() { }


        public override string ToString()
        {
            return $"Nombre: {_nombre}, Descripcion: {_descripcion}";
        }


        public void Validar() 
        {
            validarNom();
            validarDesc();
        }

        private void validarNom()
        { 
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre no puede ser vacio");
        }

        private void validarDesc()
        { 
            if (string.IsNullOrEmpty(Descripcion)) throw new Exception("La descripcion no puede ser vacia");
        }

        public override bool Equals(object obj)
        {
            TipoDeGasto t = obj as TipoDeGasto;

            if (t.Nombre == _nombre) return true;

            return false;
        }


    }
}
