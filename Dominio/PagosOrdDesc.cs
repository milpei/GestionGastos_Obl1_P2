using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PagosOrdDesc : IComparer<Pago>
    {
        public int Compare(Pago? x, Pago? y)
        {
            return x.Monto.CompareTo(y.Monto)*-1;
        }
    }
}
