﻿using Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.ServiceModel;
using System.Text.RegularExpressions;

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

    public partial class ItaliaPizzaService : ISupplierManager
    {
        public bool RegistrarProveedor(Proveedor proveedor)
        {
            var resultado = false;
            try
            {
                GestionarProveedor registrar = new GestionarProveedor();
                Logic.Proveedor nuevoProveedor = new Logic.Proveedor()
                {
                    NombreCompañia = proveedor.NombreCompañia,
                    NombreContacto = proveedor.NombreContacto,
                    Telefono = proveedor.Telefono,
                    Ciudad = proveedor.Ciudad,
                    Direccion = proveedor.Direccion
                };
                resultado = registrar.RegistrarProveedor(nuevoProveedor);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }
        public List<Proveedor> ObtenerListaProveedor()
        {
            List<Proveedor> productos = new List<Proveedor>();
            try
            {
                GestionarProveedor lista = new GestionarProveedor();
                productos = lista.CargarProveedores();
            }
            catch (EntityException e)
            {

            }
            return productos;
        }

        public bool ActualizarProveedor(int idProveedor, string nuevoNombreCompañia, string nuevoNombreContacto, string nuevoTelefono, string nuevaCiudad, string nuevaDireccion)
        {
            GestionarProveedor actualizar = new GestionarProveedor();
            var resultado = false;
            try
            {
                resultado = actualizar.ActualizarProveedor(idProveedor, nuevoNombreCompañia, nuevoNombreContacto, nuevoTelefono, nuevaCiudad, nuevaDireccion);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }

        public bool EliminarProveedor(int idProveedor)
        {
            GestionarProveedor eliminar = new GestionarProveedor();
            var resultado = false;
            try
            {
                resultado = eliminar.EliminarProveedor(idProveedor);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }

        public bool CompañiaYaExistente(string nombreCompañia)
        {
            GestionarProveedor registro = new GestionarProveedor();
            var resultado = false;
            try
            {
                resultado = registro.CompañiaYaRegistrada(nombreCompañia);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }
    }

    public partial class ItaliaPizzaService : IOrderManager
    {
        public void ActualizarPedido(int idPedido, string nombreCliente, string domicilio, string tipoPedido, decimal total, List<int> idProductos)
        {
            try
            {
                Logic.GestionarPedidos gestionarPedidos = new Logic.GestionarPedidos();
                gestionarPedidos.ActualizarPedido(idPedido, nombreCliente, domicilio, tipoPedido, total, idProductos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar el nuevo pedido: " + ex.Message);
            }
        }

        public bool ModificarEstadoDePedido(int pedidoId, string nuevoEstado)
        {
            try
            {
                Logic.GestionarPedidos gestionarPedidos = new Logic.GestionarPedidos();
                return gestionarPedidos.ModificarEstadoDePedido(pedidoId, nuevoEstado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar el estado del pedido: " + ex.Message);
                return false; 
            }
        }

        public List<int> RecuperarIdsProductosDePedido(int idPedido)
        {
            try
            {
                Logic.GestionarPedidos gestionarPedidos = new Logic.GestionarPedidos();
                return gestionarPedidos.RecuperarIdsProductosDePedido(idPedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información de los pedidos: " + ex.Message);
                return new List<int>();
            }
        }

        public List<Logic.Pedidos> RecuperarInformacionPedidos()
        {
            try
            {
                Logic.GestionarPedidos gestionarPedidos = new Logic.GestionarPedidos();
                return gestionarPedidos.RecuperarInformacionPedidos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información de los pedidos: " + ex.Message);
                return new List<Logic.Pedidos>(); 
            }
        }

        public List<Logic.Productos> RecuperarInformacionProductos(string tipo)
        {
            try
            {
                Logic.GestionarPedidos gestionarPedidos = new Logic.GestionarPedidos();
                return gestionarPedidos.RecuperarProductosPorTipo(tipo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información de los pedidos: " + ex.Message);
                return new List<Logic.Productos>();
            }
        }

        public Productos RecuperarProductoPorId(int idProducto)
        {
            try
            {
                Logic.GestionarPedidos gestionarPedidos = new Logic.GestionarPedidos();
                return gestionarPedidos.RecuperarProductoPorId(idProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar el producto por ID: " + ex.Message);
                return null;
            }
        }

        public void RegistrarNuevoPedido(string nombreCliente, int idEmpleado, string domicilio, string tipoPedido, decimal total, List<int> idProductos)
        {
            try
            {
                Logic.GestionarPedidos gestionarPedidos = new Logic.GestionarPedidos();
                gestionarPedidos.RegistrarNuevoPedido(nombreCliente, idEmpleado, domicilio, tipoPedido, total, idProductos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar el nuevo pedido: " + ex.Message);
            }
        }
    }

    public partial class ItaliaPizzaService : IInsumoManager
    {
        public bool RegistrarInsumo(Insumos insumo)
        {
            var resultado = false;
            try
            {
                GestionarInsumos registrar = new GestionarInsumos();
                Logic.Insumos nuevoInsumo = new Logic.Insumos()
                {
                    Nombre = insumo.Nombre,
                    CodigoInsumo = insumo.CodigoInsumo,
                    Marca = insumo.Marca,
                    Tipo = insumo.Tipo,
                    CantidadDeEmpaque = insumo.CantidadDeEmpaque,
                };
                resultado = registrar.Registrar(nuevoInsumo);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }

        public bool InsumoYaRegistrado(string nombre)
        {
            GestionarInsumos registro = new GestionarInsumos();
            var resultado = false;
            try
            {
                resultado = registro.InsumoYaRegistrado(nombre);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }


        public List<Insumos> ObtenerListaInsumos()
        {
            List<Insumos> insumos = new List<Insumos>();
            try
            {
                GestionarInsumos lista = new GestionarInsumos();
                insumos = lista.CargarInsumos();
            }
            catch (EntityException e)
            {

            }
            return insumos;
        }

        public bool EliminarInsumo(int idInsumo)
        {
            GestionarInsumos eliminar = new GestionarInsumos();
            var resultado = false;
            try
            {
                resultado = eliminar.EliminarInsumoSeleccionado(idInsumo);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }

        public bool ActualizarInsumo(int idInsumo, string nombre, string codigoInsumo, string marca, string tipo, string cantidadDeEmpaque)
        {
            GestionarInsumos actualizar = new GestionarInsumos();
            var resultado = false;
            try
            {
                resultado = actualizar.ActualizarInsumo(idInsumo, nombre, codigoInsumo, marca, tipo, cantidadDeEmpaque);
            }
            catch (EntityException e)
            {

            }
            return resultado;
        }
    }
}
