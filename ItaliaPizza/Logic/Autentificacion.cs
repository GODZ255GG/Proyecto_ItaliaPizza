using DataAccess;
using System.Linq;

namespace Logic
{
    public class Autentificacion
    {
        public Autentificacion()
        {
        }

        public Logic.Empleados IniciarSesion(string correo, string contraseña)
        {
            Logic.Empleados usuario = new Logic.Empleados();
            using(var context = new BDItaliaPizzaEntities())
            {
                var cuenta = context.Empleados.FirstOrDefault(e => e.correo == correo);
                if (cuenta != null && VerificarContraseña(contraseña, cuenta.contraseña))
                {
                    return new Logic.Empleados()
                    {
                        IdEmpleados = cuenta.idEmpleados,
                        Nombre = cuenta.nombre,
                        ApellidoPaterno = cuenta.apellidoPaterno,
                        ApellidoMaterno = cuenta.apellidoMaterno,
                        Telefono = cuenta.telefono,
                        Correo = cuenta.correo,
                        Foto = cuenta.foto,
                        Rol = cuenta.rol,
                        Status = true
                    };
                }
            }
            return new Logic.Empleados() { Status = false };
        }

        private bool VerificarContraseña(string contraseñaIngresada, string contraseñaAlmacenada)
        {
            if (contraseñaIngresada == contraseñaAlmacenada)
            {
                return true;
            }
            return false;
        }

    }
}
