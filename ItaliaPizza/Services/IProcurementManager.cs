using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IProcurementManager
    {
        [OperationContract]
        List<Logic.Insumos> RecuperarInsumosPorCategoria(string categoria);
        [OperationContract]
        void RegistrarNuevaCompra(int idProveedor, List<int> idInsumos);
        [OperationContract]
        List<Logic.CompraDeInventario> RecuperarInformacionCompras();
    }
}
