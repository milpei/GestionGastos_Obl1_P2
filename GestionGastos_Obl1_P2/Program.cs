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

            while (flag1)
            {
                Console.Clear();
                Console.WriteLine("Se debe ingresar un numero del menú");
                MostrarOpciones();
                //Lo manejo con las teclas directamente
                ConsoleKeyInfo opcion = Console.ReadKey(intercept: true);

                //.Key para especificar que quiero el dato key de el tipo de dato ConsoleKeyInfo.
                switch (opcion.Key)
                {

                    case ConsoleKey.D1:

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
                        Console.WriteLine("");
                        Console.WriteLine("Presione cualquier tecla para salír.");
                        Console.ReadKey();


                        break;

                    case ConsoleKey.D2:

                        bool error1 = true;
                        while (error1)
                        {
                            Console.Clear();
                            try
                            {
                                Console.WriteLine("Ingrese el Email que desea buscar");
                                string email = Console.ReadLine();

                                List<Pago> pagosUsuario = s.PagosPorUsuario(email);
                                if (pagosUsuario.Count == 0)
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
                                error1 = false;
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Presione cualquier tecla para continuar");
                            Console.ReadKey();
                        }

                        break;

                    case ConsoleKey.D3:

                        bool error2 = true;
                        while (error2)
                        {
                            Console.Clear();
                            try
                            {

                                Console.Clear();
                                Console.WriteLine("Ingrese el nombre de su nuevo usuario");
                                string nombre = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine("Ingrese el apellido de su nuevo usuario");
                                string apellido = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine("Ingrese su contraseña (al menos 8 caracteres)");
                                string contra = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Ingrese el nombre de su equipo");
                                foreach (Equipo e in s.Equipos)
                                {
                                    Console.WriteLine(e.Nombre);
                                }

                                string nomEquipo = Console.ReadLine();

                                if (s.BuscarEquipoPorNom(nomEquipo) != null)
                                {

                                    Usuario nuevoUsuario = new Usuario(nombre, apellido, contra, s.BuscarEquipoPorNom(nomEquipo), DateTime.Now);
                                    s.AgregarUsuario(nuevoUsuario);
                                    Console.WriteLine("El usuario se agregó correctamente.");
                                    error2 = false;
                                }
                                else
                                {
                                    Console.WriteLine("El equipo no existe ingreselo de nuevo");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("");
                                Console.WriteLine(ex.Message);

                            }
                            Console.WriteLine("");
                            Console.WriteLine("Presione cualquier tecla para continuar");
                            Console.ReadKey();
                        }

                        break;

                    case ConsoleKey.D4:

                        bool error3 = true;
                        while (error3)
                        {
                            Console.Clear();

                            Console.WriteLine("Ingrese el nombre del equipo");

                            if (s.Equipos.Count == 0)
                            {
                                Console.WriteLine("No hay equipos registrados");
                            }

                            try
                            {
                                {
                                    foreach (Equipo e in s.Equipos)
                                    {
                                        Console.WriteLine(e.Nombre);
                                    }
                                    string nombreDeEquipo = Console.ReadLine();
                                    Console.Clear();

                                    List<Usuario> integrantesEquipo = s.IntegrantesEquipo(nombreDeEquipo);

                                    foreach (Usuario u in integrantesEquipo)
                                    {
                                        Console.WriteLine($"{u.Nombre} {u.Apellido}, {u.Email}, {u.Equipo.Nombre}");
                                    }

                                    error3 = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("");
                                Console.WriteLine(ex.Message); 
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Presione cualquier tecla para continuar");
                            Console.ReadKey();
                        }

                        break;

                    case ConsoleKey.D5:

                        bool error4 = true;
                        while (error4)
                        {
                            Console.Clear();

                            try 
                            {
                                List<Pago> pagosDeEsteMes = s.PagosMesX(DateTime.Now);

                                if (pagosDeEsteMes.Count == 0)
                                {
                                    Console.WriteLine("No hay pagos en este mes");
                                }

                                    foreach (Pago p in pagosDeEsteMes)
                                    {
                                        Console.WriteLine(p);
                                    }

                                error4 = false;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("");
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Presione cualquier tecla para continuar");
                            Console.ReadKey();
                        }
                        break;


                    case ConsoleKey.D6:
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

                    case ConsoleKey.D7:
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


                    case ConsoleKey.Escape:
                        flag1 = false;
                        break;

                }
            }
        }


        public static void MostrarOpciones()
        {
            //Console.WriteLine("0 - Menú");
            Console.WriteLine("1 - Mostrar listado de todos los usuarios");
            Console.WriteLine("2 - Dado un correo de usuario listar todos los pagos que realizó ese usuario");
            Console.WriteLine("3 - Alta de un usuario");
            Console.WriteLine("4 - Mostrar usuarios pertenecientes al equipo");
            Console.WriteLine("5 - Ver pagos del mes actual");
            Console.WriteLine("Esc - Salir");
        }


    }
}

