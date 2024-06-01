﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GestionarEmpleados
    {
        public bool Registrar(Logic.Empleados usuario)
        {
            bool status = NuevoRegistro(usuario);
            return status;
        }

        public bool RegistrarCliente(Logic.Cliente usuario)
        {
            bool status = NuevoRegistroCliente(usuario);
            return status;
        }


        public bool NuevoRegistro(Logic.Empleados usuario)
        {
            var status = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                DataAccess.Empleados nuevoUsuario = new DataAccess.Empleados()
                {
                    nombre = usuario.Nombre,
                    apellidoPaterno = usuario.ApellidoPaterno,
                    apellidoMaterno = usuario.ApellidoMaterno,
                    telefono = usuario.Telefono,
                    correo = usuario.Correo,
                    contrasena = usuario.Contraseña,
                    foto = usuario.Foto,
                    rol = usuario.Rol
                };

                };
                context.Empleados.Add(nuevoUsuario);
                try
                {
                    status = context.SaveChanges() > 0;
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                    throw;  // Rethrow the exception to see the stack trace if needed
                }
            }
            return status;
        }

        public bool NuevoRegistroCliente(Logic.Cliente usuario)
        {
            var status = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                DataAccess.Clientes nuevoUsuario = new DataAccess.Clientes()
                {
                    nombre = usuario.Nombre,
                    telefono = usuario.Telefono,
                    rol = usuario.Rol

                };
                context.Clientes.Add(nuevoUsuario);
                status = context.SaveChanges() > 0;
            }
            return status;
        }

        public bool UsuarioYaRegistrado(string correo)
        {
            bool result = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                var usuario = (from Empleados in context.Empleados
                               where Empleados.correo == correo
                               select Empleados).Count();
                if (usuario > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool ActualizarEmpleado(int idEmpleados, string nombre, string apellidoPaterno, string apellidoMaterno, string correo, string telefono, string contraseña, string rol)
        {
            using (var context = new BDItaliaPizzaEntities())
            {
                var query = (from Empleados in context.Empleados
                             where Empleados.idEmpleados == idEmpleados
                             select Empleados).FirstOrDefault();
                if (query != null)
                {
                    query.nombre = nombre;
                    query.apellidoPaterno = apellidoPaterno;
                    query.apellidoMaterno = apellidoMaterno;
                    query.correo = correo;
                    query.telefono = telefono;
                    query.contrasena = contraseña;
                    query.rol = rol;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool EliminarEmpleados(int idEmpleados)
        {
            var status = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                var usuario = context.Empleados.FirstOrDefault(p => p.idEmpleados == idEmpleados);
                if (usuario != null)
                {
                    context.Empleados.Remove(usuario);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }

        public List<Empleados> CargarUsuarios()
        {
            List<Empleados> listaUsuarios = new List<Empleados>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var usuariosDetalle = (from Empleados in context.Empleados
                                           select new Empleados
                                           {
                                               IdEmpleados = Empleados.idEmpleados,
                                               Nombre = Empleados.nombre,
                                               ApellidoPaterno = Empleados.apellidoPaterno,
                                               ApellidoMaterno = Empleados.apellidoMaterno,
                                               Correo = Empleados.correo,
                                               Contraseña = Empleados.contrasena,
                                               Foto = Empleados.foto,
                                               Rol = Empleados.rol
                                           });
                    listaUsuarios.AddRange(usuariosDetalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaUsuarios;
        }
    }
}
