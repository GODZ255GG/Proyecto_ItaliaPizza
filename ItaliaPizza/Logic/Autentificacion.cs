using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var cuentas = (from empleado in context.Empleados
                               where empleado.correo == correo && empleado.contraseña == contraseña
                               select empleado);
                if (cuentas.Any())
                {
                    usuario.IdEmpleados = cuentas.First().idEmpleados;
                    usuario.Nombre = cuentas.First().nombre;
                    usuario.ApellidoPaterno = cuentas.First().apellidoPaterno;
                    usuario.ApellidoMaterno = cuentas.First().apellidoMaterno;
                    usuario.Telefono = cuentas.First().telefono;
                    usuario.Correo = cuentas.First().correo;
                    usuario.Contraseña = cuentas.First().contraseña;
                    usuario.Foto = cuentas.First().foto;
                    usuario.Rol = cuentas.First().rol;
                    usuario.Status = true;
                }
            }
            return usuario;
        }

        public bool ExisteCorreoYContraseña(string correo, string contraseña)
        {
            using (var context = new BDItaliaPizzaEntities())
            {
                return context.Empleados.Any(em => em.correo == correo 
                                            && em.contraseña == contraseña);
            }
        }

    }
}
