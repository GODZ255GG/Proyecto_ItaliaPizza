using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class GestionarProductos
    {
        public bool Registrar(Logic.Productos producto)
        {
            bool resultado = NuevoRegistro(producto);
            return resultado;
        }

        public bool NuevoRegistro(Logic.Productos producto)
        {
            var resultado = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                DataAccess.Productos nuevoProducto = new DataAccess.Productos()
                {
                    nombre = producto.Nombre,
                    codigoProducto = producto.CodigoProducto,
                    marca = producto.Marca,
                    tipo = producto.Tipo,
                    precio = producto.Precio
                };
                context.Productos.Add(nuevoProducto);
                resultado = context.SaveChanges() > 0;
            }
            return resultado;
        }

        public bool ProductoYaRegistrado(string nombre)
        {
            bool resultado = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                var producto = (from productos in context.Productos
                                where productos.nombre == nombre
                                select productos).Count();
                if (producto > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public List<Productos> CargarProductos()
        {
            List<Productos> listaProductos = new List<Productos>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var productosDetalle = (from producto in context.Productos
                                            select new Productos
                                            {
                                                IdProductos = producto.idProductos,
                                                Nombre = producto.nombre,
                                                CodigoProducto = producto.codigoProducto,
                                                Marca = producto.marca,
                                                Tipo = producto.tipo,
                                                Precio = (double)producto.precio,
                                            });
                    listaProductos.AddRange(productosDetalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaProductos;
        }

        public bool EliminarProductoSeleccionado(int idProducto)
        {
            var resultado = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                var producto = context.Productos.FirstOrDefault(p => p.idProductos == idProducto);
                if (producto != null)
                {
                    context.Productos.Remove(producto);
                    context.SaveChanges();
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            return resultado;
        }

        public bool ActualizarProducto(int idProducto, string nuevoNombre, string nuevoCodigo, string nuevaMarca, string nuevoTipo, double nuevoPrecio)
        {
            using (var context = new BDItaliaPizzaEntities())
            {
                var query = (from productos in context.Productos
                                            where productos.idProductos == idProducto
                                            select productos).FirstOrDefault();
                if (query != null)
                {
                    query.nombre = nuevoNombre;
                    query.codigoProducto = nuevoCodigo;
                    query.marca = nuevaMarca;
                    query.tipo = nuevoTipo;
                    query.precio = nuevoPrecio;
                    context.SaveChanges();
                    return true;
                }
                
                
            }
            return false;
        }
    }
}
