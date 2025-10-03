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



        // DATOS DE PRECARGA;

        // =======================
        // EQUIPOS (4)
        // =======================
        Equipo eFrontend = new Equipo("Frontend");
        Equipo eBackend = new Equipo("Backend");
        Equipo eData = new Equipo("Data");
        Equipo eUX = new Equipo("UX");

        // =======================
        // TIPOS DE GASTO (10)
        // =======================
        TipoDeGasto tAuto = new TipoDeGasto("Auto", "Gastos del vehículo y arreglos");
        TipoDeGasto tAfters = new TipoDeGasto("Afters", "Salidas y afters del equipo");
        TipoDeGasto tOficina = new TipoDeGasto("Oficina", "Insumos y material de oficina");
        TipoDeGasto tNube = new TipoDeGasto("Nube", "Servicios cloud y hosting");
        TipoDeGasto tSuscr = new TipoDeGasto("Suscripciones", "Plataformas y software");
        TipoDeGasto tCapacitacion = new TipoDeGasto("Capacitacion", "Cursos y certificaciones");
        TipoDeGasto tViajes = new TipoDeGasto("Viajes", "Viáticos, pasajes y traslados");
        TipoDeGasto tHardware = new TipoDeGasto("Hardware", "Equipamiento y repuestos");
        TipoDeGasto tComidas = new TipoDeGasto("Comidas", "Almuerzos y reuniones");
        TipoDeGasto tEventos = new TipoDeGasto("Eventos", "Entradas y conferencias");

        // =======================
        // USUARIOS (22)
        // Nota: ctor: Usuario(string nombre, string apellido, string contra, Equipo equipo, DateTime fInc)
        // =======================
        Usuario u1 = new Usuario("Ana", "Pérez", "PassSegura1", eFrontend, new DateTime(2024, 03, 12));
        Usuario u2 = new Usuario("Bruno", "López", "PassSegura1", eFrontend, new DateTime(2024, 05, 03));
        Usuario u3 = new Usuario("Camila", "Suárez", "PassSegura1", eFrontend, new DateTime(2023, 11, 21));
        Usuario u4 = new Usuario("Diego", "Pereyra", "PassSegura1", eFrontend, new DateTime(2023, 09, 15));
        Usuario u5 = new Usuario("Eva", "García", "PassSegura1", eFrontend, new DateTime(2024, 02, 02));

        Usuario u6 = new Usuario("Facundo", "Silva", "PassSegura1", eBackend, new DateTime(2023, 12, 05));
        Usuario u7 = new Usuario("Gabriel", "Ramos", "PassSegura1", eBackend, new DateTime(2024, 04, 18));
        Usuario u8 = new Usuario("Helena", "Méndez", "PassSegura1", eBackend, new DateTime(2023, 10, 09));
        Usuario u9 = new Usuario("Iván", "Castro", "PassSegura1", eBackend, new DateTime(2024, 01, 27));
        Usuario u10 = new Usuario("Julia", "Morales", "PassSegura1", eBackend, new DateTime(2023, 08, 30));
        Usuario u11 = new Usuario("Kevin", "Sosa", "PassSegura1", eBackend, new DateTime(2024, 06, 06));

        Usuario u12 = new Usuario("Lucía", "Fernández", "PassSegura1", eData, new DateTime(2023, 07, 14));
        Usuario u13 = new Usuario("Matías", "Alonso", "PassSegura1", eData, new DateTime(2024, 03, 08));
        Usuario u14 = new Usuario("Nicolás", "Vega", "PassSegura1", eData, new DateTime(2024, 02, 25));
        Usuario u15 = new Usuario("Olivia", "Martínez", "PassSegura1", eData, new DateTime(2023, 11, 03));
        Usuario u16 = new Usuario("Pablo", "Aguiar", "PassSegura1", eData, new DateTime(2024, 01, 12));

        Usuario u17 = new Usuario("Rocío", "Pintos", "PassSegura1", eUX, new DateTime(2024, 05, 21));
        Usuario u18 = new Usuario("Santiago", "Costa", "PassSegura1", eUX, new DateTime(2023, 12, 19));
        Usuario u19 = new Usuario("Tamara", "De León", "PassSegura1", eUX, new DateTime(2023, 09, 07));
        Usuario u20 = new Usuario("Ulises", "Piñeyro", "PassSegura1", eUX, new DateTime(2024, 02, 14));
        Usuario u21 = new Usuario("Valentina", "Cabrera", "PassSegura1", eUX, new DateTime(2024, 06, 01));
        Usuario u22 = new Usuario("Walter", "Cuevas", "PassSegura1", eUX, new DateTime(2023, 10, 28));


        // =======================
        // PAGOS (42 en total)
        //  - Recurrentes: 25 (5 totalmente pagados, 10 activos con fin, 10 sin fin)
        //  - Únicos: 17
        // Nota: ctor Recurrente(DateTime ini, DateTime fin, MetodosDePago mp, TipoDeGasto tg, Usuario u, string desc, decimal monto)
        //       ctor Unico(DateTime fpago, string nroRecibo, MetodosDePago mp, TipoDeGasto tg, Usuario u, string desc, decimal monto)
        // =======================

        // ----- 5 Recurrentes TOTALMENTE PAGOS (FFin < hoy 2025-10-03) -----
        // 5 Recurrentes CERRADOS
        Pago p1 = new Recurrente(new DateTime(2024, 01, 01), new DateTime(2025, 06, 01), MetodosDePago.Credito, tNube, u6, "Azure DevOps (cerrado)", 1200m);
        Pago p2 = new Recurrente(new DateTime(2024, 03, 01), new DateTime(2025, 07, 01), MetodosDePago.Debito, tSuscr, u1, "Figma Org (cerrado)", 800m);
        Pago p3 = new Recurrente(new DateTime(2023, 12, 01), new DateTime(2025, 08, 01), MetodosDePago.Efectivo, tCapacitacion, u13, "Udemy for Business (cerrado)", 950m);
        Pago p4 = new Recurrente(new DateTime(2024, 02, 01), new DateTime(2025, 05, 01), MetodosDePago.Credito, tOficina, u10, "Limpieza mensual (cerrado)", 500m);
        Pago p5 = new Recurrente(new DateTime(2024, 04, 01), new DateTime(2025, 09, 01), MetodosDePago.Debito, tNube, u12, "S3 Backups (cerrado)", 1100m);

        // 10 Recurrentes ACTIVOS con FIN
        Pago p6 = new Recurrente(new DateTime(2025, 01, 01), new DateTime(2025, 12, 01), MetodosDePago.Credito, tNube, u2, "AWS EC2", 2200m);
        Pago p7 = new Recurrente(new DateTime(2024, 11, 01), new DateTime(2026, 02, 01), MetodosDePago.Debito, tSuscr, u17, "Atlassian Suite", 1350m);
        Pago p8 = new Recurrente(new DateTime(2025, 05, 01), new DateTime(2026, 05, 01), MetodosDePago.Efectivo, tCapacitacion, u5, "Scrum Alliance", 700m);
        Pago p9 = new Recurrente(new DateTime(2024, 12, 01), new DateTime(2026, 01, 01), MetodosDePago.Credito, tViajes, u7, "Traslados mensuales", 1500m);
        Pago p10 = new Recurrente(new DateTime(2025, 03, 01), new DateTime(2026, 03, 01), MetodosDePago.Debito, tHardware, u8, "Soporte impresoras", 900m);
        Pago p11 = new Recurrente(new DateTime(2025, 02, 01), new DateTime(2026, 04, 01), MetodosDePago.Efectivo, tComidas, u9, "Almuerzos sprint", 600m);
        Pago p12 = new Recurrente(new DateTime(2025, 06, 01), new DateTime(2026, 01, 01), MetodosDePago.Credito, tEventos, u3, "Meetup mensual", 450m);
        Pago p13 = new Recurrente(new DateTime(2025, 07, 01), new DateTime(2026, 06, 01), MetodosDePago.Debito, tOficina, u4, "Plantas y café", 300m);
        Pago p14 = new Recurrente(new DateTime(2025, 04, 01), new DateTime(2026, 02, 01), MetodosDePago.Credito, tAfters, u18, "After retro", 500m);
        Pago p15 = new Recurrente(new DateTime(2025, 08, 01), new DateTime(2026, 08, 01), MetodosDePago.Debito, tNube, u11, "CDN mensual", 1600m);

        // 10 Recurrentes SIN FIN (FFin = DateTime.MinValue)
        Pago p16 = new Recurrente(new DateTime(2024, 10, 01), DateTime.MinValue, MetodosDePago.Credito, tNube, u14, "Cloud Logs", 700m);
        Pago p17 = new Recurrente(new DateTime(2025, 01, 01), DateTime.MinValue, MetodosDePago.Debito, tSuscr, u15, "JetBrains All Products", 1200m);
        Pago p18 = new Recurrente(new DateTime(2024, 09, 01), DateTime.MinValue, MetodosDePago.Efectivo, tComidas, u16, "Fruta para oficina", 250m);
        Pago p19 = new Recurrente(new DateTime(2025, 02, 01), DateTime.MinValue, MetodosDePago.Credito, tViajes, u19, "Cabify interno", 800m);
        Pago p20 = new Recurrente(new DateTime(2025, 03, 01), DateTime.MinValue, MetodosDePago.Debito, tHardware, u20, "Garantías extendidas", 950m);
        Pago p21 = new Recurrente(new DateTime(2024, 12, 01), DateTime.MinValue, MetodosDePago.Credito, tEventos, u21, "Charlas semanales", 300m);
        Pago p22 = new Recurrente(new DateTime(2025, 04, 01), DateTime.MinValue, MetodosDePago.Efectivo, tOficina, u22, "Papelería base", 180m);
        Pago p23 = new Recurrente(new DateTime(2025, 05, 01), DateTime.MinValue, MetodosDePago.Credito, tAfters, u1, "After quincenal", 400m);
        Pago p24 = new Recurrente(new DateTime(2025, 06, 01), DateTime.MinValue, MetodosDePago.Debito, tCapacitacion, u2, "Pluralsight", 950m);
        Pago p25 = new Recurrente(new DateTime(2025, 07, 01), DateTime.MinValue, MetodosDePago.Efectivo, tNube, u12, "Monitorización", 1000m);

        // 17 Pagos ÚNICOS
        Pago p26 = new Unico(new DateTime(2025, 09, 12), "REC-1001", MetodosDePago.Efectivo, tComidas, u3, "Almuerzo planning", 420m);
        Pago p27 = new Unico(new DateTime(2025, 08, 28), "REC-1002", MetodosDePago.Credito, tHardware, u4, "Mouse ergonómico", 950m);
        Pago p28 = new Unico(new DateTime(2025, 08, 05), "REC-1003", MetodosDePago.Debito, tEventos, u5, "Entrada meetup JS", 300m);
        Pago p29 = new Unico(new DateTime(2025, 09, 02), "REC-1004", MetodosDePago.Credito, tViajes, u6, "Taxi a cliente", 680m);
        Pago p30 = new Unico(new DateTime(2025, 07, 19), "REC-1005", MetodosDePago.Debito, tOficina, u7, "Resma de hojas", 250m);
        Pago p31 = new Unico(new DateTime(2025, 09, 20), "REC-1006", MetodosDePago.Efectivo, tSuscr, u8, "Add-on puntual", 500m);
        Pago p32 = new Unico(new DateTime(2025, 08, 15), "REC-1007", MetodosDePago.Credito, tCapacitacion, u9, "Examen certificación", 1500m);
        Pago p33 = new Unico(new DateTime(2025, 09, 07), "REC-1008", MetodosDePago.Debito, tAfters, u10, "Pizza cierre sprint", 380m);
        Pago p34 = new Unico(new DateTime(2025, 09, 25), "REC-1009", MetodosDePago.Credito, tAuto, u11, "Peaje visita cliente", 180m);
        Pago p35 = new Unico(new DateTime(2025, 08, 22), "REC-1010", MetodosDePago.Efectivo, tOficina, u12, "Cables HDMI", 420m);
        Pago p36 = new Unico(new DateTime(2025, 09, 14), "REC-1011", MetodosDePago.Debito, tHardware, u13, "SSD externo", 3200m);
        Pago p37 = new Unico(new DateTime(2025, 07, 03), "REC-1012", MetodosDePago.Credito, tEventos, u14, "Taller de UX", 2100m);
        Pago p38 = new Unico(new DateTime(2025, 08, 10), "REC-1013", MetodosDePago.Efectivo, tComidas, u15, "Desayuno demo", 290m);
        Pago p39 = new Unico(new DateTime(2025, 09, 03), "REC-1014", MetodosDePago.Debito, tNube, u16, "IP fija adicional", 850m);
        Pago p40 = new Unico(new DateTime(2025, 08, 29), "REC-1015", MetodosDePago.Credito, tViajes, u17, "Uber retrospectiva", 510m);
        Pago p41 = new Unico(new DateTime(2025, 09, 18), "REC-1016", MetodosDePago.Debito, tOficina, u18, "Toner impresora", 2100m);
        Pago p42 = new Unico(new DateTime(2025, 09, 27), "REC-1017", MetodosDePago.Credito, tSuscr, u19, "Créditos API puntuales", 1200m);



        // DE TODOS ESTOS DATOS HAY QUE ARMAR LAS LIST ,QUE LOS CONTENGAS Y DEJAR 2 O 3 DE CADA TIPO AFUERA PARA PROBAR LOS METODOS AGREGAR.


        // MAÑANA TERMINAR LOS METODOS AGREGAR, CREAR LAS LISTAS Y PROBAR LAS FUNCIONES.



    }
}

