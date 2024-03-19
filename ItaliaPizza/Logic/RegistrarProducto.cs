using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class RegistrarProducto
    {
        public bool Registrar(Logic.Productos producto)
        {
            bool status = NuevoRegistro(producto);
            return status;
        }

        public bool NuevoRegistro(Logic.Productos producto)
        {
            var status = false;
            using (var context = new ItaliaPizzaEntities())
            {
                DataAccess.Productos nuevoProducto = new DataAccess.Productos()
                {
                    nombre = producto.Nombre,
                    marca = producto.Marca,
                    tipo = producto.Tipo,
                    foto = producto.Foto,
                    precio = producto.Precio,
                    stock = producto.Stock
                };
                context.Productos.Add(nuevoProducto);
                status = context.SaveChanges() > 0;
            }
            return status;
        }

        public bool ProductoYaRegistrado(string nombre)
        {
            bool result = false;
            using (var context = new ItaliaPizzaEntities())
            {
                var producto = (from productos in context.Productos
                                where productos.nombre == nombre
                                select productos).Count();
                if (producto > 0)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
