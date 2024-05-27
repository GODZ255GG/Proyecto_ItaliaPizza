using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class GestionarCompraDeInventario
    {
        public List<Productos> RecuperarProductosPorCategoria(string categoria)
        {
            List<Productos> listaProductos = new List<Productos>();

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var productosPorTipo = (from pr in context.Productos
                                            where pr.tipo == categoria
                                            select new Productos
                                            {
                                                IdProductos = pr.idProductos,
                                                Nombre = pr.nombre
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
    }
}
