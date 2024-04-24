using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract]
    public interface IOrderManager
    {
        [OperationContract]
        void RegistrarNuevoPedido(string nombreCliente, int idEmpleado, string domicilio, string tipoPedido, decimal total, List<int> idProductos);

        [OperationContract]
        List<Logic.Pedidos> RecuperarInformacionPedidos();

        [OperationContract]
        bool ModificarEstadoDePedido(int pedidoId, string nuevoEstado);

        [OperationContract]
        List<Logic.Productos> RecuperarInformacionProductos(string tipo);
        [OperationContract]
        void ActualizarPedido(int idPedido, string nombreCliente, string domicilio, string tipoPedido, decimal total, List<int> idProductos);
        [OperationContract]
        List<int> RecuperarIdsProductosDePedido(int idPedido);
        [OperationContract]
        Logic.Productos RecuperarProductoPorId(int idProducto);
    }
}
