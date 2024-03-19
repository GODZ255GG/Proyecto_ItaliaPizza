using Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public partial class ItaliaPizzaService : IProductManager
    {
        public bool RegistrarProducto(Productos producto)
        {
            var status = false;
            try
            {
                RegistrarProducto registrar = new RegistrarProducto();
                Logic.Productos nuevoProducto = new Logic.Productos()
                {
                    Nombre = producto.Nombre,
                    Marca = producto.Marca,
                    Tipo = producto.Tipo,
                    Foto = producto.Foto,
                    Precio = producto.Precio,
                    Stock = producto.Stock
                };
                status = registrar.Registrar(nuevoProducto);
            }
            catch (EntityException e)
            {

            }
            return status;
        }

        public bool ProductoYaRegistrado(string nombre)
        {
            RegistrarProducto registro = new RegistrarProducto();
            var status = false;
            try
            {
                status = registro.ProductoYaRegistrado(nombre);
            }
            catch (EntityException e)
            {

            }
            return status;
        }
    }

    public partial class ItaliaPizzaService : IUserManager
    {
        public Logic.Usuarios IniciarSesion(String correo, String contraseña)
        {
            var usuario = new Logic.Usuarios()
            {
                Status = false
            };
            try
            {
                var client = new Autentificacion();
                usuario = client.IniciarSesion(correo, contraseña);
            }
            catch (EntityException e)
            {

            }
            return usuario;
        }
    }
}
