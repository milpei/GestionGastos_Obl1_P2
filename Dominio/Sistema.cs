using Dominio.Entidades;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios;
        private List<Equipo> _equipos;
        private List<Pago> _pagos;
        private List<TipoDeGasto> _tiposDeGasto;

        public List<Usuario> Usuarios { get { return new List<Usuario>(_usuarios); } }
        public List<Equipo> Equipos { get { return new List<Equipo>(_equipos); } }
        public List<Pago> Pagos { get { return new List<Pago>(_pagos); } }
        public List<TipoDeGasto> TiposDeGasto { get { return new List<TipoDeGasto>(_tiposDeGasto); } }

        public Sistema()
        {
            _usuarios = new List<Usuario>();
            _equipos = new List<Equipo>();
            _pagos = new List<Pago>();
            _tiposDeGasto = new List<TipoDeGasto>();
        }

        public void AgregarUsuario(Usuario u) 
        {
            u.Validar();
            while (_usuarios.Contains(u)) u.CambiarEmail(); // valida el mail del ususario, si este ya existe lo ingresa con un mail nuevo.
            _usuarios.Add(u);
        }
// Hay un problema en el pienso del sistema, el usuario se diferencia por el mail, pero para yo chequear correctamente si el ususario no existe deberia tener la cedula del mismo.


        public void AgregarEquipo(Equipo e)
        {
            e.Validar();
            if (_equipos.Contains(e)) throw new Exception ("El equipo ya existe");
            _equipos.Add(e);
        }

        public void AgregarTipoDeGasto(TipoDeGasto t)
        {
            t.Validar();
            if (_tiposDeGasto.Contains(t)) throw new Exception("El Tipo de Gasto ya existe");
            _tiposDeGasto.Add(t);
        }

        public void AgregarPago(Pago p)
        {
            p.Validar();
            _pagos.Add(p);
        }

        public List<Usuario> UsuariosPorEquipo(int idEquipo)
        {
            List<Usuario> retorno = new List<Usuario>();

            foreach (Usuario u in _usuarios)
            {
                if (u.Equipo.Id == idEquipo) retorno.Add(u);
            }

            return retorno;
        }

        public Equipo BuscarEquipoPorNom(string NomEquipo) 
        {
            foreach (Equipo e in _equipos) 
            {
                if (e.Equals(NomEquipo)) return e;
            }

            return null;
        }

        public  List<Pago> PagosPorUsuario(string email) 
        {
            if (string.IsNullOrEmpty(email)) throw new Exception("El email no puede se vacío");

            List<Pago> ret = new List<Pago>();

            foreach (Pago p in _pagos)
            {
                if (p.Usuario.Email == email) ret.Add(p);
            }
            return ret;
        }

        public List<Usuario> IntegrantesEquipo(string nombreDeEquipo) {

            List<Usuario> ret = new List<Usuario>();

            if (string.IsNullOrEmpty(nombreDeEquipo)) throw new Exception("El nombre no puede ser vacío");

                foreach (Usuario u in _usuarios)
                {
                if (u.Equipo.Nombre == nombreDeEquipo) ret.Add(u);
                }

            return ret;
        }

        public TipoDeGasto ObtenerTipoGastoPorNombre(string tipoGasto) 
        {
        foreach (TipoDeGasto t in _tiposDeGasto)
            {
                if (t.Nombre == tipoGasto) return t;
            }
            return null;
        }

        public Usuario ObtenerUsuarioPorMail(string email)
        {
            foreach (Usuario u in _usuarios)
            {
                if (u.Email == email) return u;
            }
            return null;
        }


        /*
        public string PagosMesActual()
        {
            List<Pago> _pagosMesActual = new List<Pago>();

            foreach (Pago p in _pagos)
            {
                if (p.Fe)
            }
        }
        */




    }
}

