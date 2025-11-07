using Dominio.Entidades;


namespace Dominio
{
    public class PagosOrdAsc : IComparer<Pago>
    {
        public int Compare(Pago? x, Pago? y)
        {
            return x.Monto.CompareTo(y.Monto);
        }
    }
}
