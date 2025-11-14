using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class UsuarioMailOrdAsc : IComparer<Usuario>
    {
        public int Compare(Usuario x, Usuario y)
        {
            return x.Email.CompareTo(y.Email) * 1;
        }
    }
}
