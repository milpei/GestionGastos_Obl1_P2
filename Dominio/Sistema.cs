using Dominio.Entidades;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios;
        private List<Equipo> _equipos;
        private List<Pago> _pagos;
        private List<TipoDeGasto> _tiposDeGasto;
        private static Sistema _instancia;


        public List<Usuario> Usuarios { get { return new List<Usuario>(_usuarios); } }
        public List<Equipo> Equipos { get { return new List<Equipo>(_equipos); } }
        public List<Pago> Pagos { get { return new List<Pago>(_pagos); } }
        public List<TipoDeGasto> TiposDeGasto { get { return new List<TipoDeGasto>(_tiposDeGasto); } }
        public static Sistema Instancia
        {
            get 
            {
                if (_instancia == null) _instancia = new Sistema();
                return _instancia;
            }
        }

        public Sistema()
        {
            _usuarios = new List<Usuario>();
            _equipos = new List<Equipo>();
            _pagos = new List<Pago>();
            _tiposDeGasto = new List<TipoDeGasto>();


            PrecargarDatos();

        }
       

        public void AgregarUsuario(Usuario u)
        {
            GenerarEmail(u);
            u.Validar();
            _usuarios.Add(u);
        }


        public void AgregarEquipo(Equipo e)
        {
            e.Validar();
            if (_equipos.Contains(e)) throw new Exception("El equipo ya existe");
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


        public Equipo BuscarEquipoPorNom(string nomEquipo)
        {
            Equipo ret = null;

            foreach (Equipo e in _equipos)
            {
                if (e.Nombre.ToLower() == nomEquipo.ToLower()) ret = e;
            }

            if (ret == null) throw new Exception("El equipo no existe");

            return ret;

        }

        public List<Pago> PagosPorUsuario(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new Exception("El email no puede se vacío");

            Usuario u = ObtenerUsuarioPorMail(email);

            if (!Usuarios.Contains(u)) throw new Exception("El usuario no existe");


            List<Pago> ret = new List<Pago>();

            foreach (Pago p in _pagos)
            {
                if (p.Usuario.Email.ToLower() == email.ToLower()) ret.Add(p);
            }

            return ret;
        }

        public List<Usuario> IntegrantesEquipo(string nombreDeEquipo)
        {

            List<Usuario> ret = new List<Usuario>();

            Equipo e = BuscarEquipoPorNom(nombreDeEquipo);

            if (!Equipos.Contains(e)) throw new Exception("El Equipo no existe");
            if (string.IsNullOrEmpty(nombreDeEquipo)) throw new Exception("El nombre no puede ser vacío");

            foreach (Usuario u in _usuarios)
            {
                if (u.Equipo.Nombre.ToLower() == nombreDeEquipo.ToLower()) ret.Add(u);
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
                if (u.Email.ToLower() == email.ToLower()) return u;
            }
            return null;
        }


        public Usuario GenerarEmail(Usuario u)
        {
            int cont = 0;
            string nom = u.Nombre.Substring(0, Math.Min(3, u.Nombre.Length));
            string ape = u.Apellido.Substring(0, Math.Min(3, u.Apellido.Length));
            string inicioMail = nom + ape;
            u.Email = inicioMail.ToLower() + "@laEmpresa.com";

            foreach (Usuario us in _usuarios)
            {
                if ((nom.ToLower() == us.Nombre.Substring(0, Math.Min(3, us.Nombre.Length)).ToLower()) && ape.ToLower() == us.Apellido.Substring(0, Math.Min(3, us.Apellido.Length)).ToLower()) cont++;
            }

            if (cont > 0) u.Email = inicioMail.ToLower() + cont + "@laEmpresa.com";

            return u;
        }
        //LO CAMBIE, CHEQUEAR Y ACTUALIZAR

        public List<Pago> PagosMesX(DateTime fecha)
        {
            List<Pago> pagosMesX = new List<Pago>();

            foreach (Pago p in _pagos)
            {
                if (p.EsDelMesX(fecha)) pagosMesX.Add(p);
            }

            return pagosMesX;
        }

        //DATOS DE PRECARGA;

        private void PrecargarDatos()
        {
            // ===== Campos de referencia (a nivel de clase) =====

            // Instancias reutilizables
            Equipo eFrontend = new Equipo("Frontend");
            Equipo eBackend = new Equipo("Backend");
            Equipo eData = new Equipo("Data");
            Equipo eUX = new Equipo("UX");

            // Lista
            List<Equipo> _equiposPrecarga = new List<Equipo>
            {
                eFrontend, eBackend, eData, eUX
            };

            foreach (Equipo e in _equiposPrecarga)
            {
                AgregarEquipo(e);
            }

            // Instancias reutilizables
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

            // Lista
            List<TipoDeGasto> _tiposDeGastoPrecarga = new List<TipoDeGasto>
            {
             tAuto, tAfters, tOficina, tNube, tSuscr, tCapacitacion, tViajes, tHardware, tComidas, tEventos
            };

            foreach (TipoDeGasto t in _tiposDeGastoPrecarga)
            {
                AgregarTipoDeGasto(t);
            }

            //AGREGAR UN USUARIO CON UN NOMBRE DE 2 O UNA LETRA.
            // Instancias reutilizables (usan los equipos de arriba)
            Usuario u1 = new Usuario("Ana", "Perez", "PassSegura1", eFrontend, new DateTime(2024, 03, 12));
            Usuario u2 = new Usuario("bruno", "Rodriguez", "PassSegura2", eFrontend, new DateTime(2024, 05, 03));
            Usuario u3 = new Usuario("Ana", "Perez", "PassSegura3", eFrontend, new DateTime(2023, 11, 21));

            Usuario u4 = new Usuario("Diego", "Pereyra", "PassSegura4", eFrontend, new DateTime(2023, 09, 15));
            Usuario u5 = new Usuario("yi", "Garcia", "PassSegura5", eFrontend, new DateTime(2024, 02, 02));

            Usuario u6 = new Usuario("Agustin", "Tejera", "PassSegura6", eBackend, new DateTime(2023, 12, 05));
            Usuario u7 = new Usuario("ana", "perez", "PassSegura7", eBackend, new DateTime(2024, 04, 18));
            Usuario u8 = new Usuario("Helena", "Mendez", "PassSegura8", eBackend, new DateTime(2023, 10, 09));
            Usuario u9 = new Usuario("Ivan", "Castro", "PassSegura9", eBackend, new DateTime(2024, 01, 27));
            Usuario u10 = new Usuario("Julia", "Morales", "PassSegura10", eBackend, new DateTime(2023, 08, 30));
            Usuario u11 = new Usuario("Kevin", "Sosa", "PassSegura11", eBackend, new DateTime(2024, 06, 06));

            Usuario u12 = new Usuario("Lucia", "Fernandez", "PassSegura12", eData, new DateTime(2023, 07, 14));
            Usuario u13 = new Usuario("Matias", "Alonso", "PassSegura13", eData, new DateTime(2024, 03, 08));
            Usuario u14 = new Usuario("Nicolas", "Vega", "PassSegura14", eData, new DateTime(2024, 02, 25));
            Usuario u15 = new Usuario("Olivia", "Martinez", "PassSegura15", eData, new DateTime(2023, 11, 03));
            Usuario u16 = new Usuario("Pablo", "Aguiar", "PassSegura16", eData, new DateTime(2024, 01, 12));

            Usuario u17 = new Usuario("Rocio", "Pintos", "PassSegura17", eUX, new DateTime(2024, 05, 21));
            Usuario u18 = new Usuario("Santiago", "Costa", "PassSegura18", eUX, new DateTime(2023, 12, 19));
            Usuario u19 = new Usuario("Tamara", "De Leon", "PassSegura19", eUX, new DateTime(2023, 09, 07));
            Usuario u20 = new Usuario("Ulises", "Piñeyro", "PassSegura20", eUX, new DateTime(2024, 02, 14));
            Usuario u21 = new Usuario("Valentina", "Cabrera", "PassSegura21", eUX, new DateTime(2024, 06, 01));
            Usuario u22 = new Usuario("Walter", "Cuevas", "PassSegura22", eUX, new DateTime(2023, 10, 28));

            // Lista
            List<Usuario> _usuariosPrecarga = new List<Usuario>
            {
            u1,u2,u3,u4,u5,u6,u7,u8,u9,u10,u11,u12,u13,u14,u15,u16,u17,u18,u19,u20,u21,u22
            };

            foreach (Usuario u in _usuariosPrecarga)
            {
                AgregarUsuario(u);
            }



            List<Pago> _pagosPrecarga = new List<Pago>
            {
            // 5 Recurrentes CERRADOS
            new Recurrente(new DateTime(2024, 01, 01), new DateTime(2025, 06, 01), MetodosDePago.Credito,  tNube,         u6,  "Azure DevOps (cerrado)",         1200m),
            new Recurrente(new DateTime(2024, 03, 01), new DateTime(2025, 07, 01), MetodosDePago.Debito,   tSuscr,        u1,  "Figma Org (cerrado)",             800m),
            new Recurrente(new DateTime(2023, 12, 01), new DateTime(2025, 08, 01), MetodosDePago.Efectivo, tCapacitacion, u13, "Udemy for Business (cerrado)",    950m),
            new Recurrente(new DateTime(2024, 02, 01), new DateTime(2025, 05, 01), MetodosDePago.Credito,  tOficina,      u10, "Limpieza mensual (cerrado)",       500m),
            new Recurrente(new DateTime(2024, 04, 01), new DateTime(2025, 09, 01), MetodosDePago.Debito,   tNube,         u12, "S3 Backups (cerrado)",            1100m),

        // 10 Recurrentes ACTIVOS con FIN
            new Recurrente(new DateTime(2025, 01, 01), new DateTime(2025, 12, 01), MetodosDePago.Credito,  tNube,         u2,  "AWS EC2",                        2200m),
            new Recurrente(new DateTime(2024, 11, 01), new DateTime(2026, 02, 01), MetodosDePago.Debito,   tSuscr,        u17, "Atlassian Suite",                 1350m),
            new Recurrente(new DateTime(2025, 05, 01), new DateTime(2026, 05, 01), MetodosDePago.Efectivo, tCapacitacion, u5,  "Scrum Alliance",                   700m),
            new Recurrente(new DateTime(2024, 12, 01), new DateTime(2026, 01, 01), MetodosDePago.Credito,  tViajes,       u7,  "Traslados mensuales",             1500m),
            new Recurrente(new DateTime(2025, 03, 01), new DateTime(2026, 03, 01), MetodosDePago.Debito,   tHardware,     u8,  "Soporte impresoras",               900m),
            new Recurrente(new DateTime(2025, 02, 01), new DateTime(2026, 04, 01), MetodosDePago.Efectivo, tComidas,      u9,  "Almuerzos sprint",                 600m),
            new Recurrente(new DateTime(2025, 06, 01), new DateTime(2026, 01, 01), MetodosDePago.Credito,  tEventos,      u3,  "Meetup mensual",                   450m),
            new Recurrente(new DateTime(2025, 07, 01), new DateTime(2026, 06, 01), MetodosDePago.Debito,   tOficina,      u4,  "Plantas y café",                   300m),
            new Recurrente(new DateTime(2025, 04, 01), new DateTime(2026, 02, 01), MetodosDePago.Credito,  tAfters,       u18, "After retro",                      500m),
            new Recurrente(new DateTime(2025, 08, 01), new DateTime(2026, 08, 01), MetodosDePago.Debito,   tNube,         u11, "CDN mensual",                     1600m),

            // 10 Recurrentes SIN FIN (FFin = DateTime.MinValue)
            new Recurrente(new DateTime(2024, 10, 01), DateTime.MinValue, MetodosDePago.Credito,  tNube,         u14, "Cloud Logs",              700m),
            new Recurrente(new DateTime(2025, 01, 01), DateTime.MinValue, MetodosDePago.Debito,   tSuscr,        u15, "JetBrains All Products", 1200m),
            new Recurrente(new DateTime(2024, 09, 01), DateTime.MinValue, MetodosDePago.Efectivo, tComidas,      u16, "Fruta para oficina",      250m),
            new Recurrente(new DateTime(2025, 02, 01), DateTime.MinValue, MetodosDePago.Credito,  tViajes,       u19, "Cabify interno",          800m),
            new Recurrente(new DateTime(2025, 03, 01), DateTime.MinValue, MetodosDePago.Debito,   tHardware,     u20, "Garantías extendidas",    950m),
            new Recurrente(new DateTime(2024, 12, 01), DateTime.MinValue, MetodosDePago.Credito,  tEventos,      u21, "Charlas semanales",       300m),
            new Recurrente(new DateTime(2025, 04, 01), DateTime.MinValue, MetodosDePago.Efectivo, tOficina,      u22, "Papelería base",          180m),
            new Recurrente(new DateTime(2025, 05, 01), DateTime.MinValue, MetodosDePago.Credito,  tAfters,       u1,  "After quincenal",         400m),
            new Recurrente(new DateTime(2025, 06, 01), DateTime.MinValue, MetodosDePago.Debito,   tCapacitacion, u2,  "Pluralsight",              950m),
            new Recurrente(new DateTime(2025, 07, 01), DateTime.MinValue, MetodosDePago.Efectivo, tNube,         u12, "Monitorización",         1000m),

        // 17 Pagos ÚNICOS
            new Unico(new DateTime(2025, 09, 12), "REC-1001", MetodosDePago.Efectivo, tComidas,      u3,  "Almuerzo planning",       420m),
            new Unico(new DateTime(2025, 08, 28), "REC-1002", MetodosDePago.Credito,  tHardware,     u4,  "Mouse ergonómico",        950m),
            new Unico(new DateTime(2024, 08, 05), "REC-1003", MetodosDePago.Debito,   tEventos,      u5,  "Entrada meetup JS",       300m),
            new Unico(new DateTime(2025, 09, 02), "REC-1004", MetodosDePago.Credito,  tViajes,       u6,  "Taxi a cliente",          680m),
            new Unico(new DateTime(2025, 07, 19), "REC-1005", MetodosDePago.Debito,   tOficina,      u7,  "Resma de hojas",          250m),
            new Unico(new DateTime(2025, 10, 20), "REC-1006", MetodosDePago.Efectivo, tSuscr,        u8,  "Add-on puntual",          500m),
            new Unico(new DateTime(2025, 08, 15), "REC-1007", MetodosDePago.Credito,  tCapacitacion, u9,  "Examen certificación",    1500m),
            new Unico(new DateTime(2025, 09, 07), "REC-1008", MetodosDePago.Debito,   tAfters,       u10, "Pizza cierre sprint",     380m),
            new Unico(new DateTime(2024, 09, 25), "REC-1009", MetodosDePago.Credito,  tAuto,         u11, "Peaje visita cliente",    180m),
            new Unico(new DateTime(2025, 08, 22), "REC-1010", MetodosDePago.Efectivo, tOficina,      u12, "Cables HDMI",             420m),
            new Unico(new DateTime(2025, 10, 14), "REC-1011", MetodosDePago.Debito,   tHardware,     u13, "SSD externo",            3200m),
            new Unico(new DateTime(2025, 07, 03), "REC-1012", MetodosDePago.Credito,  tEventos,      u14, "Taller de UX",           2100m),
            new Unico(new DateTime(2025, 08, 10), "REC-1013", MetodosDePago.Efectivo, tComidas,      u15, "Desayuno demo",           290m),
            new Unico(new DateTime(2025, 09, 03), "REC-1014", MetodosDePago.Debito,   tNube,         u16, "IP fija adicional",       850m),
            new Unico(new DateTime(2025, 08, 29), "REC-1015", MetodosDePago.Credito,  tViajes,       u17, "Uber retrospectiva",      510m),
            new Unico(new DateTime(2025, 09, 18), "REC-1016", MetodosDePago.Debito,   tOficina,      u18, "Toner impresora",        2100m),
            new Unico(new DateTime(2025, 09, 27), "REC-1017", MetodosDePago.Credito,  tSuscr,        u19, "Créditos API puntuales", 1200m),
            };

            foreach (Pago p in _pagosPrecarga)
            {
                AgregarPago(p);
            }


        }

        public Usuario Login(string email, string pwd)
        {
            Usuario u = ObtenerUsuarioPorMail(email);

            if (u != null)
            {
                if (u.Contrasenia.Equals(pwd))
                {
                    return u;
                }
            }

            throw new Exception("Datos incorrectos");

        }
    }
}

