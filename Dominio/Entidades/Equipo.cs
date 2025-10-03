using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.Entidades
{
    public class Equipo
    {
        private static int s_UltId = 0;
        private int _id;
        public string Nombre { get; set; }
        public int Id { get { return _id; } }

        public Equipo (string nombre)
        {
            Nombre = nombre;
            _id = ++s_UltId; 
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre} ID: {_id}" ;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre no puede ser vacio");
        }

        public override bool Equals(object obj)
        {
            Equipo e = obj as Equipo;

            if (e.Nombre == Nombre) return true;

            return false;
        }

    }
}
