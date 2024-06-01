using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GestionarPedidos
    {

        public List<Pedidos> RecuperarInformacionPedidos()
        {
            List<Pedidos> listaPedidos = new List<Pedidos>();

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var pedidosConDetalles = (from p in context.Pedidos
                                              join dp in context.DetallesPedido on p.idPedidos equals dp.Pedidos_idPedidos
                                              join e in context.Empleados on dp.Empleados_idEmpleados equals e.idEmpleados
                                              group new { p, dp, e } by new
                                              {
                                                  p.idPedidos,
                                                  p.estadoDelPedido,
                                                  p.domicilioDeEntrega,
                                                  p.tipoDePedido,
                                                  dp.fechaHoraDelPedido,
                                                  p.nombreCliente,
                                                  p.precioTotal
                                              } into g
                                              select new
                                              {
                                                  PedidoId = g.Key.idPedidos,
                                                  EstadoPedido = g.Key.estadoDelPedido,
                                                  NombreCliente = g.Key.nombreCliente,
                                                  DomicilioDeEntrega = g.Key.domicilioDeEntrega,
                                                  tipoDePedido = g.Key.tipoDePedido,
                                                  FechaHoraDelPedido = g.Key.fechaHoraDelPedido,
                                                  Total = g.Key.precioTotal,
                                                  Productos = (from pp in context.PedidoProducto
                                                               join pr in context.Productos on pp.Productos_idProductos equals pr.idProductos
                                                               where pp.Pedidos_idPedidos == g.Key.idPedidos
                                                               select new
                                                               {
                                                                   Nombre = pr.nombre,
                                                                   Precio = pr.precio
                                                               }).ToList()
                                              }).ToList();

                    foreach (var pedidoDetalle in pedidosConDetalles)
                    {
                        var productosDetalle = pedidoDetalle.Productos
                            .GroupBy(x => new { x.Nombre, x.Precio })
                            .Select(grp => new
                            {
                                Nombre = grp.Key.Nombre,
                                Precio = grp.Key.Precio,
                                Cantidad = grp.Count()
                            });

                        string productosConcatenados = string.Join("", productosDetalle.Select(p => $"{p.Nombre} (${p.Precio}) x{p.Cantidad}\n"));

                        listaPedidos.Add(new Logic.Pedidos
                        {
                            IdPedidos = pedidoDetalle.PedidoId,
                            EstadoDelPedido = pedidoDetalle.EstadoPedido,
                            NombreDelCliente = pedidoDetalle.NombreCliente,
                            DomicilioDeEntrega = pedidoDetalle.DomicilioDeEntrega,
                            TipoDePedido = pedidoDetalle.tipoDePedido,
                            FechaPedido = (DateTime)pedidoDetalle.FechaHoraDelPedido,
                            Total = Convert.ToDouble(pedidoDetalle.Total),
                            ProductosConcatenados = productosConcatenados
                        });
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información de los pedidos: " + ex.Message);
            }

            return listaPedidos;
        }



        public void RegistrarNuevoPedido(string nombreCliente, int idEmpleado, string domicilio, string tipoPedido, decimal total, List<int> idProductos)
        {
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            DataAccess.Pedidos nuevoPedido = new DataAccess.Pedidos
                            {
                                tipoDePedido = tipoPedido,
                                domicilioDeEntrega = domicilio,
                                estadoDelPedido = "En preparación",
                                precioTotal = (double?)total,
                                nombreCliente = nombreCliente
                            };
                            context.Pedidos.Add(nuevoPedido);
                            context.SaveChanges();

                            int idPedido = nuevoPedido.idPedidos;

                            foreach (int idProducto in idProductos)
                            {
                                PedidoProducto pedidoProducto = new PedidoProducto
                                {
                                    Productos_idProductos = idProducto,
                                    Pedidos_idPedidos = idPedido
                                };
                                context.PedidoProducto.Add(pedidoProducto);
                            }

                            DetallesPedido detallesPedido = new DetallesPedido
                            {
                                Pedidos_idPedidos = idPedido,
                                Empleados_idEmpleados = idEmpleado,
                                fechaHoraDelPedido = DateTime.Now
                            };
                            context.DetallesPedido.Add(detallesPedido);

                            context.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al registrar el nuevo pedido.", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar el nuevo pedido: " + ex.Message);
            }
        }



        public List<Logic.Productos> RecuperarProductosPorTipo(string tipo)
        {
            List<Logic.Productos> listaProductos = new List<Logic.Productos>();

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var productosPorTipo = (from pr in context.Productos
                                            where pr.tipo == tipo
                                            select new Logic.Productos
                                            {
                                                IdProductos = pr.idProductos,
                                                Nombre = pr.nombre,
                                                Precio = pr.precio ?? 0
                                            }).ToList();

                    listaProductos.AddRange(productosPorTipo); // Agregar los productos de productosPorTipo a listaProductos
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información de los productos por tipo: " + ex.Message);
            }

            return listaProductos;
        }


        public bool ModificarEstadoDePedido(int pedidoId, string nuevoEstado)
        {
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var pedido = context.Pedidos.FirstOrDefault(p => p.idPedidos == pedidoId);
                    if (pedido != null)
                    {
                        pedido.estadoDelPedido = nuevoEstado;
                        context.SaveChanges();
                        return true; 
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró el pedido con ID: {pedidoId}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el estado del pedido: " + ex.Message);
                return false; 
            }
        }

        public void ActualizarPedido(int idPedido, string nombreCliente, string tipoPedido, string domicilio, decimal total, List<int> idProductos)
        {
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var pedidoExistente = context.Pedidos.FirstOrDefault(p => p.idPedidos == idPedido);
                            if (pedidoExistente != null)
                            {
                                pedidoExistente.tipoDePedido = tipoPedido;
                                pedidoExistente.domicilioDeEntrega = domicilio;
                                pedidoExistente.precioTotal = (double?)total;
                                pedidoExistente.nombreCliente = nombreCliente;

                                var productosAsociados = context.PedidoProducto.Where(pp => pp.Pedidos_idPedidos == idPedido);
                                context.PedidoProducto.RemoveRange(productosAsociados);

                                foreach (int idProducto in idProductos)
                                {
                                    PedidoProducto pedidoProducto = new PedidoProducto
                                    {
                                        Productos_idProductos = idProducto,
                                        Pedidos_idPedidos = idPedido
                                    };
                                    context.PedidoProducto.Add(pedidoProducto);
                                }

                                context.SaveChanges();
                                transaction.Commit();
                            }
                            else
                            {
                                throw new Exception("No se encontró el pedido con el ID proporcionado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al actualizar el pedido.", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el pedido: " + ex.Message);
            }
        }

        public List<int> RecuperarIdsProductosDePedido(int idPedido)
        {
            List<int> idsProductos = new List<int>();

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    idsProductos = (from pp in context.PedidoProducto
                                    where pp.Pedidos_idPedidos == idPedido
                                    select pp.Productos_idProductos).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar los IDs de productos asociados al pedido: " + ex.Message);
            }

            return idsProductos;
        }

        public Logic.Productos RecuperarProductoPorId(int idProducto)
        {
            Logic.Productos producto = null;

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var productoEncontrado = (from pr in context.Productos
                                              where pr.idProductos == idProducto
                                              select new Logic.Productos
                                              {
                                                  IdProductos = pr.idProductos,
                                                  Nombre = pr.nombre,
                                                  Precio = pr.precio ?? 0
                                              }).FirstOrDefault();

                    if (productoEncontrado != null)
                    {
                        producto = productoEncontrado;
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún producto con el ID especificado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información del producto por ID: " + ex.Message);
            }

            return producto;
        }

        public List<int> RecuperarIdsProductosDePedidos()
        {
            List<int> idsProductos = new List<int>();

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    idsProductos = (from pp in context.PedidoProducto
                                    select pp.Productos_idProductos).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar los IDs de productos asociados a los pedidos: " + ex.Message);
            }

            return idsProductos;
        }

    }
}