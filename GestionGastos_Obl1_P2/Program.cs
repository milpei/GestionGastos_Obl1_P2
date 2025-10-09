using Dominio;
using Dominio.Entidades;

namespace GestionGastos_Obl1_P2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Sistema s = new Sistema();

            bool flag = true;

            while (flag)
            {
                MostrarOpciones();
                if (!int.TryParse(Console.ReadLine(), out int opcion)) Console.WriteLine("Se debe ingresar un numero");
                Console.Clear();

                switch (opcion)
                {
                    case 0:
                        flag = false;
                        break;

                    case 1:
                        foreach (Usuario u in s.Usuarios)
                        {
                            Console.WriteLine(u.Nombre, u.Email, u.Equipo.ToString());
                        }

                        break;

                    case 2:
                        Console.WriteLine("Ingrese el Email que desea buscar");
                        string email = Console.ReadLine();

                        List<Pago> pagosUsuario = s.PagosPorUsuario(email);

                        foreach (Pago p in pagosUsuario)
                        {
                            Console.WriteLine(p.ToString());
                        }

                        break;

                    case 3:
                        Console.WriteLine("Ingrese el nombre de su nuevo usuario");
                        string nombre = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Ingrese el apellido de su nuevo usuario");
                        string apellido = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Ingrese su contraseña");
                        string contra = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Ingrese el nombre de su equipo");
                        while (flag)
                        {
                            foreach (Equipo e in s.Equipos)
                            {
                                Console.WriteLine(e.Nombre);
                            }

                            string nomEquipo = Console.ReadLine();

                            if (s.BuscarEquipoPorNom(nomEquipo) != null)
                            {

                                flag = false;
                                Usuario nuevoUsuario = new Usuario(nombre, apellido, contra, s.BuscarEquipoPorNom(nomEquipo), DateTime.Now);
                                s.AgregarUsuario(nuevoUsuario);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("El equipo no existe ingreselo de nuevo");
                            }


                        }

                        break;

                    case 4:
                        Console.WriteLine("Ingrese el nombre del equipo");
                        string nombreDeEquipo = Console.ReadLine();

                        List<Usuario> integrantesEquipo = s.IntegrantesEquipo(nombreDeEquipo);

                        foreach (Usuario u in integrantesEquipo)
                        {
                            Console.WriteLine(u.Nombre, u.Email);
                        }

                        break;

                    case 5:
                        Console.WriteLine("Ingrese una fecha de inicio dd/mm/aaaa");
                        DateTime.TryParse(Console.ReadLine(), out DateTime fInicio);

                        Console.WriteLine("Ingrese una fecha de fin dd/mm/aaaa");
                        DateTime.TryParse(Console.ReadLine(), out DateTime fFin);

                        Console.WriteLine("0 - Credito, 1 - Debito, 2 - Efectivo");
                        MetodosDePago.TryParse(Console.ReadLine(), out MetodosDePago metodoPagoRecurrente);

                        Console.WriteLine("Elija el tipo de gasto");
                        foreach (TipoDeGasto t in s.TiposDeGasto)
                        {
                            Console.WriteLine($"Nombre: {t.Nombre} Descripcion: {t.Descripcion} ");
                        }
                        string tipoGastoR = Console.ReadLine();

                        Console.WriteLine("Ingrese su email");
                        string emailUsuarioR = Console.ReadLine();

                        Console.WriteLine("Ingrese una descripcion");
                        string descripcionR = Console.ReadLine();

                        Console.WriteLine("Ingrese un monto");
                        Decimal.TryParse(Console.ReadLine(), out decimal montoR);


                        Pago pagoRecurrente = new Recurrente(fInicio, fFin, metodoPagoRecurrente, s.ObtenerTipoGastoPorNombre(tipoGastoR), s.ObtenerUsuarioPorMail(emailUsuarioR), descripcionR, montoR);
                        s.AgregarPago(pagoRecurrente);

                        //Creo que hay que validar aca, que la finicio.month != fFin.month y que el metodo >= 0 && <= 2
                        break;

                    case 6:
                        Console.WriteLine("Ingrese una fecha del pago dd/mm/aaaa");
                        DateTime.TryParse(Console.ReadLine(), out DateTime fPago);

                        Console.WriteLine("Ingrese el numero de Recibo");
                        string numRecibo = Console.ReadLine();

                        Console.WriteLine("0 - Credito, 1 - Debito, 2 - Efectivo");
                        MetodosDePago.TryParse(Console.ReadLine(), out MetodosDePago metodoPagoUnico);

                        Console.WriteLine("Elija el tipo de gasto");
                        foreach (TipoDeGasto t in s.TiposDeGasto)
                        {
                            Console.WriteLine($"Nombre: {t.Nombre} Descripcion: {t.Descripcion} ");
                        }
                        string tipoGastoU = Console.ReadLine();

                        Console.WriteLine("Ingrese su email");
                        string emailUsuarioU = Console.ReadLine();

                        Console.WriteLine("Ingrese una descripcion");
                        string descripcionU = Console.ReadLine();

                        Console.WriteLine("Ingrese un monto");
                        Decimal.TryParse(Console.ReadLine(), out decimal montoU);


                        Pago pagoUnico = new Unico(fPago, numRecibo, metodoPagoUnico, s.ObtenerTipoGastoPorNombre(tipoGastoU), s.ObtenerUsuarioPorMail(emailUsuarioU), descripcionU, montoU);
                        s.AgregarPago(pagoUnico);

                        //Creo que hay que validar aca, que la finicio.month != fFin.month y que el metodo >= 0 && <= 2
                        break;
                }
            }
        }


        public static void MostrarOpciones()
        {
            Console.WriteLine("0 - Salir");
            Console.WriteLine("1 - Mostrar listado de todos los usuarios");
            Console.WriteLine("2 - Dado un correo de usuario listar todos los pagos que realizó ese usuario");
            Console.WriteLine("3 - Alta de un usuario");
            Console.WriteLine("4 - Mostrar usuarios pertenecientes al equipo");
            Console.WriteLine("5 - Agregar pago Recurrente");
            Console.WriteLine("6 - Agregar pago Unico");
        }


    }
}

