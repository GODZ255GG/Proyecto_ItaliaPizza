using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ListaProductos
    {
        public List<Productos> CargarProductos()
        {
            List<Productos> listaProductos = new List<Productos>();
            try
            {
                using(var context = new ItaliaPizzaEntities())
                {
                    var productosDetalle = (from producto in context.Productos
                                            select new Productos
                                            {
                                                IdProductos = producto.idProductos,
                                                Nombre = producto.nombre,
                                                Marca = producto.marca,
                                                Tipo = producto.tipo,
                                                Precio = (double)producto.precio,
                                                Stock = (int)producto.stock
                                            });
                    listaProductos.AddRange(productosDetalle);                 
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaProductos;
        }  
    }
}
