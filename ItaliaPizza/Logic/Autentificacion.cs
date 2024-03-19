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

        public Logic.Usuarios IniciarSesion(string correo, string contraseña)
        {
            Logic.Usuarios usuario = new Logic.Usuarios();
            using(var context = new ItaliaPizzaEntities())
            {
                var cuentas = (from usuarios in context.Usuarios
                               where usuarios.correo == correo && usuarios.contraseña == contraseña
                               select usuarios);
                if (cuentas.Any())
                {
                    usuario.IdUsuarios = cuentas.First().idUsuarios;
                    usuario.Nombre = cuentas.First().nombre;
                    usuario.ApellidoPaterno = cuentas.First().apellidoPaterno;
                    usuario.ApellidoMaterno = cuentas.First().apellidoMaterno;
                    usuario.Telefono = (int)cuentas.First().telefono;
                    usuario.Correo = cuentas.First().correo;
                    usuario.Contraseña = "";
                    usuario.Foto = cuentas.First().foto;
                    usuario.Rol = cuentas.First().rol;
                    usuario.Status = true;
                }
            }
            return usuario;
        }
    }
}
