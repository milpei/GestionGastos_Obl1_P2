using Dominio;
using Dominio.Entidades;

namespace GestionGastos_Obl1_P2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Sistema s = new Sistema();

            bool flag1 = true;
            int opcion = 0;

            while (flag1)
            {
                switch (opcion)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Se debe ingresar un numero del menú");
                        MostrarOpciones();
                        int.TryParse(Console.ReadLine(), out opcion);

                        break;

                    case 1:
                        Console.Clear();

                        if (s.Usuarios.Count == 0)
                        {
                            Console.WriteLine("No existen usuarios registrados");
                        }
                        else
                        {
                            foreach (Usuario u in s.Usuarios)
                            {
                                Console.WriteLine($"{u.Nombre} {u.Apellido}, {u.Email}, {u.Equipo.Nombre}");
                            }
                        } 
                        Console.ReadKey();
                        opcion = 0;

                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ingrese el Email que desea buscar");
                        string email = Console.ReadLine();

                        List<Pago> pagosUsuario = s.PagosPorUsuario(email);
                        if (pagosUsuario.Count == 0 )
                        {
                            Console.WriteLine("Este usuario no realizó ningún pago");
                        }
                        else
                        {
                            foreach (Pago p in pagosUsuario)
                            {
                                Console.WriteLine(p.ToString());
                            }
                        }

                        Console.ReadKey();
                        opcion = 0;
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Ingrese el nombre de su nuevo usuario");
                        string nombre = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Ingrese el apellido de su nuevo usuario");
                        string apellido = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Ingrese su contraseña (al menos 8 caracteres)");
                        string contra = Console.ReadLine();
                        bool flag2 = true;
                        while (flag2)
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el nombre de su equipo");
                            foreach (Equipo e in s.Equipos)
                            {
                                Console.WriteLine(e.Nombre);
                            }

                            string nomEquipo = Console.ReadLine();

                            if (s.BuscarEquipoPorNom(nomEquipo) != null)
                            {

                                flag2 = false;
                                Usuario nuevoUsuario = new Usuario(nombre, apellido, contra, s.BuscarEquipoPorNom(nomEquipo), DateTime.Now);
                                s.AgregarUsuario(nuevoUsuario);
                                Console.WriteLine("El usuario se agregó correctamente.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("El equipo no existe ingreselo de nuevo");
                            }
                        }
                        Console.ReadKey();
                        opcion = 0;
                        break;

                    case 4:
                        Console.Clear();
                        bool flag3 = true;
                        while (flag3)
                        {
                            Console.WriteLine("Ingrese el nombre del equipo");

                            if (s.Equipos.Count == 0)
                            {
                                Console.WriteLine("No hay equipos registrados");
                            }
                            else
                            {
                                foreach (Equipo e in s.Equipos)
                                {
                                    Console.WriteLine(e.Nombre);
                                }
                                string nombreDeEquipo = Console.ReadLine();

                                List<Usuario> integrantesEquipo = s.IntegrantesEquipo(nombreDeEquipo);

                                if (s.BuscarEquipoPorNom(nombreDeEquipo) == null)
                                {
                                    Console.Clear();
                                    Console.WriteLine("El equipo no existe, ingreselo nuevamente");
                                    Console.ReadKey();
                                    Console.Clear();
                                    opcion = 4;
                                }
                                else
                                {
                                    Console.Clear();
                                    foreach (Usuario u in integrantesEquipo)
                                    {
                                        Console.WriteLine($"{u.Nombre} {u.Apellido}, {u.Email}, {u.Equipo.Nombre}");
                                    }
                                    flag3 = false;
                                }
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        opcion = 0;
                        break;

                    case 5:
                        Console.Clear();
                        List<Pago> pagosDeEsteMes = s.PagosMesX(DateTime.Now);

                        if (pagosDeEsteMes.Count == 0)
                        {
                            Console.WriteLine("No hay pagos en este mes");
                        }
                        else
                        {
                            foreach (Pago p in pagosDeEsteMes)
                            {
                                Console.WriteLine(p);
                            }
                        }

                        Console.ReadKey();
                        opcion = 0;
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

                    case 7:
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


                    case 99:
                        flag1 = false;
                        break;

                    default:
                        Console.Clear();
                        MostrarOpciones();
                        int.TryParse(Console.ReadLine(), out opcion);

                        break;
                }
            }
        }


        public static void MostrarOpciones()
        {
            Console.WriteLine("0 - Menú");
            Console.WriteLine("1 - Mostrar listado de todos los usuarios");
            Console.WriteLine("2 - Dado un correo de usuario listar todos los pagos que realizó ese usuario");
            Console.WriteLine("3 - Alta de un usuario");
            Console.WriteLine("4 - Mostrar usuarios pertenecientes al equipo");
            Console.WriteLine("5 - Ver pagos del mes actual");
            Console.WriteLine("99 - Salir");
        }


    }
}

