using Logic;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IInsumoManager
    {
        [OperationContract]
        bool RegistrarInsumo(Insumos insumo);
        [OperationContract]
        bool InsumoYaRegistrado(string nombre);
        [OperationContract]
        List<Insumos> ObtenerListaInsumos();
        [OperationContract]
        bool EliminarInsumo(int idInsumo);
        [OperationContract]
        bool ActualizarInsumo(int idInsumo, string nombre, string codigoInsumo, string marca, string tipo, string cantidadDeEmpaque, string unidadDeMedida);
    }
}