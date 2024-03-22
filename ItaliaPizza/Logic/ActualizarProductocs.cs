using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ActualizarProductocs
    {
        public bool EliminarProducto(int idProducto)
        {
            var status = false;
            using (var context = new ItaliaPizzaEntities())
            {
                var producto = context.Productos.FirstOrDefault(p => p.idProductos == idProducto);
                if (producto != null)
                {
                    context.Productos.Remove(producto);
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
    }
}
