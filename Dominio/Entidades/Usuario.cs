using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.Entidades
{
    public class Usuario : IValidar
    {
        private Cargo _cargo = Cargo.Gerente;
        

        //cargo de prueba

        public Cargo Cargo { get { return _cargo; } set { _cargo = value; } }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public Equipo Equipo { get; set; }
        public DateTime FIncorporacion { get; set; }
 



        public Usuario(string nombre, string apellido, string contra, Equipo equipo, DateTime fechaIncorporacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Contrasenia = contra;
            Equipo = equipo;
            FIncorporacion = fechaIncorporacion;

            
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


        public override string ToString()
        {
            return $"Nombre:{Nombre}, Apellido {Apellido}, Contraseña {Contrasenia}, Email{Email}, Equipo: {Equipo}, Fecha de incorporacion: {FIncorporacion}";
        }

        public override bool Equals(object obj)
        {
            Usuario u = obj as Usuario;

            if (this.Email.ToLower() == u.Email.ToLower()) return true;

            return false;
        }
        




    }
}

