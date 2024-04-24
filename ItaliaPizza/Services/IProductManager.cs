using Logic;
using System.Collections.Generic;
using System.ServiceModel;

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
        [OperationContract]
        bool ActualizarProducto(int idProducto, string nombre, string codigoProducto, string marca, string tipo, double precio);
    }
}
