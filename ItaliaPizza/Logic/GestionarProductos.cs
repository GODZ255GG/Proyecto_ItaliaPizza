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

        public bool RegistrarStockProducto(int idProducto, int cantidad)
        {
            bool resultado = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                // Busca el producto en la base de datos
                var producto = context.Productos.FirstOrDefault(p => p.idProductos == idProducto);

                if (producto != null)
                {
                    // Verifica si ya existe un registro en InventarioDeProductos para este producto
                    var inventarioProducto = context.InventarioDeProductos.FirstOrDefault(i => i.Productos_idProductos == idProducto);

                    if (inventarioProducto != null)
                    {
                        // Si ya existe, actualiza la cantidad directamente
                        inventarioProducto.cantidadTotal = cantidad;
                    }
                    else
                    {
                        // Verifica si existe un idInventario válido
                        var inventarioExistente = context.Inventario.FirstOrDefault();
                        if (inventarioExistente == null)
                        {
                            // Si no existe un idInventario válido, crea uno
                            inventarioExistente = new DataAccess.Inventario()
                            {
                                cantidadMaxima = 100, // Asigna un valor adecuado
                                cantidadMinima = 0   // Asigna un valor adecuado
                            };
                            context.Inventario.Add(inventarioExistente);
                            context.SaveChanges();  // Guarda los cambios para obtener el idInventario
                        }

                        // Ahora crea el nuevo registro en InventarioDeProductos
                        var nuevoInventarioProducto = new DataAccess.InventarioDeProductos()
                        {
                            Productos_idProductos = idProducto,
                            cantidadTotal = cantidad,
                            Inventario_idInventario = inventarioExistente.idInventario // Asigna el idInventario válido
                        };
                        context.InventarioDeProductos.Add(nuevoInventarioProducto);
                    }
                    context.SaveChanges();
                    resultado = true;
                }
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

        public List<InventarioDeProductos> InventarioDeProductos()
        {
            List<InventarioDeProductos> listaStock = new List<InventarioDeProductos>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var productosConInventario = (from producto in context.Productos
                                                  join inventario in context.InventarioDeProductos
                                                  on producto.idProductos equals inventario.Productos_idProductos
                                                  select new InventarioDeProductos
                                                  {
                                                      IdProductos = producto.idProductos,
                                                      CantidadTotal = inventario.cantidadTotal ?? 0
                                                  }).ToList();

                    listaStock.AddRange(productosConInventario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaStock;
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

        public InventarioDeProductos ObtenerInventarioDeProducto(int idProducto)
        {
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var inventario = context.InventarioDeProductos
                        .FirstOrDefault(i => i.Productos_idProductos == idProducto);

                    if (inventario != null)
                    {
                        return new InventarioDeProductos
                        {
                            IdProductos = inventario.Productos_idProductos,
                            CantidadTotal = inventario.cantidadTotal ?? 0
                        };
                    }
                    return null; // Retorna null si no hay inventario para el producto
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar el inventario del producto: " + ex.Message);
                return null;
            }
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
