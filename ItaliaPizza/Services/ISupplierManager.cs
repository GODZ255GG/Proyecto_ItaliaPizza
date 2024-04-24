using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract]
    public interface ISupplierManager
    {
        [OperationContract]
        bool RegistrarProveedor(Proveedor proveedor);
        [OperationContract]
        List<Proveedor> ObtenerListaProveedor();
        [OperationContract]
        bool EliminarProveedor(int idProveedor);
        [OperationContract]
        bool ActualizarProveedor(int idProveedor, string nuevoNombreCompañia, string nuevoNombreContacto, string nuevoTelefono, string nuevaCiudad, string nuevaDireccion);
        [OperationContract]
        bool CompañiaYaExistente(string nombreCompañia);
    }
}
