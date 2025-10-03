using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.Entidades
{
    public class Usuario
    {
        private string _email = "";
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; }
        public Equipo Equipo { get; set; }
        public DateTime FIncorporacion { get; set; }

        public Usuario(string nombre, string apellido, string contra, Equipo equipo, DateTime fechaIncorporacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Contrasenia = contra;
            Equipo = equipo;
            FIncorporacion = fechaIncorporacion;
            _email = GenerarEmail();
        }

        public override string ToString()
        {
            return $"Nombre:{Nombre}, Apellido {Apellido}, Contraseña {Contrasenia}, Email{Email}, Equipo: {Equipo}, Fecha de incorporacion: {FIncorporacion}";
        }

        public void Validar() 
        {
            ValidarNom();
            ValidarApe();
            ValidarContVacia();
            ValidarContCantCaracteres();
            ValidarEquipo();
            ValidarFechaVacia();
            //Será necesario hacer validacion para que fecha no sea fututra? No se pide en letra.
            ValidarEmail();

        }

        private void ValidarNom() 
        { 
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El Nombre no puede estar vacío");
        }
        private void ValidarApe()
        {
            if (string.IsNullOrEmpty(Apellido)) throw new Exception("El Apellido no puede estar vacio");
        }
        private void ValidarContVacia()
        {
            if (string.IsNullOrEmpty(Contrasenia)) throw new Exception("La Contraseña no puede estar vacía");
        }
        private void ValidarContCantCaracteres()
        {
            if (Contrasenia.Length < 8) throw new Exception ("La cotraseña debe tener al menos 8 caracteres");
        }
        private void ValidarEquipo()
        {
            if (Equipo == null) throw new Exception("El usario debe tener un equipo asignado");
        }
        private void ValidarFechaVacia()
        {
            if (FIncorporacion == DateTime.MinValue) throw new Exception("Se debe ingresar una fecha de incorpóracion ");
        }
        private void ValidarEmail()
        {
            if (string.IsNullOrEmpty(Email)) throw new Exception("El Email no puede estar vacio");
        }



        private string GenerarEmail() // Esta debe devolver string porque el constructor precisa qwue le devuelvas un dato para el asignarselo a el atributo.
        {
            return Nombre.Substring(0, Math.Min(3, Nombre.Length)) + Apellido.Substring(0, Math.Min(3, Apellido.Length)) + "@laEmpresa.com";
            //Math.Min(x,y) compara los valores y me devuelve el mas chico, entonces, si el nombre es menor a 3 Substring me devuelve el total del nombre.
        }

           public void CambiarEmail() 
        {
            Random numRandom = new Random();
            int numMail = numRandom.Next(1, 100);
            //esta combinacion de Random y Next me devueklve un num entre los valores indicados, en este caso entre 1 y 99.

            _email = Nombre.Substring(0, Math.Min(3, Nombre.Length)) + Apellido.Substring(0, Math.Min(3, Nombre.Length)) + numMail + "@laEmpresa.com";
        }

        /*
       En substring el primer numero indica la posicion y el segundo cuantos caracteres (0,3) = tres caracteres contando desde 0;
       Si escribimos Substring(x) va desde x hasta el final

       */

        
        public override bool Equals(object obj)
        {
            Usuario u = obj as Usuario;

            if (u.Email == _email) return true;

            return false;
        }
        




    }
}

