using Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.ServiceModel;

namespace Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public partial class ItaliaPizzaService : IProductManager
    {
        public bool RegistrarProducto(Productos producto)
        {
            var resultado = false;
            try
            {
                GestionarProductos registrar = new GestionarProductos();
                Logic.Productos nuevoProducto = new Logic.Productos()
                {
                    Nombre = producto.Nombre,
                    CodigoProducto = producto.CodigoProducto,
                    Marca = producto.Marca,
                    Tipo = producto.Tipo,
                    Precio = producto.Precio,
                };
                resultado = registrar.Registrar(nuevoProducto);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }

        public bool ProductoYaRegistrado(string nombre)
        {
            GestionarProductos registro = new GestionarProductos();
            var resultado = false;
            try
            {
                resultado = registro.ProductoYaRegistrado(nombre);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }


        public List<Productos> ObtenerListaProductos()
        {
            List<Productos> productos = new List<Productos>();
            try
            {
                GestionarProductos lista = new GestionarProductos();
                productos = lista.CargarProductos();
            }catch (EntityException e)
            {

            }
            return productos;
        }

        public bool EliminarProducto(int idProducto)
        {
            GestionarProductos eliminar = new GestionarProductos();
            var resultado = false;
            try
            {
                resultado = eliminar.EliminarProductoSeleccionado(idProducto);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }

        public bool ActualizarProducto(int idProducto, string nombre, string codigoProducto, string marca, string tipo, double precio)
        {
            GestionarProductos actualizar = new GestionarProductos();
            var resultado = false;
            try
            {
                resultado = actualizar.ActualizarProducto(idProducto, nombre, codigoProducto, marca, tipo, precio);
            }
            catch(EntityException e)
            {

            }
            return resultado;
        }
    }

    public partial class ItaliaPizzaService : IUserManager
    {
        public Logic.Empleados IniciarSesion(String correo, String contraseña)
        {
            var usuario = new Logic.Empleados()
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
