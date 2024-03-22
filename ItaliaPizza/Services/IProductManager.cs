using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{
    [ServiceContract]
    public interface IProductManager
    {
        [OperationContract]
        bool RegistrarProducto(Productos producto);
        [OperationContract]
        bool ProductoYaRegistrado(string nombre);
        [OperationContract]
        List<Productos> ObtenerListaProductos();
        [OperationContract]
        bool EliminarProducto(int idProducto);
    }
}
