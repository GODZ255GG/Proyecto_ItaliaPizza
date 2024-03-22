using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Productos
    {
        public static Productos ProductosClient {  get; set; }

        public int IdProductos { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public float Precio { get; set; }
        public int Stock { get; set; }
        public string Foto { get; set; }
    }
}
