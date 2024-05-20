using Logic;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IRecipeManager
    {

        [OperationContract]
        bool RegistrarReceta(Recetas receta);
        [OperationContract]
        bool RecetaYaRegistrado(string nombre);
        [OperationContract]
        List<Recetas> ObtenerListaReceta();
        [OperationContract]
        bool EliminarReceta(int idReceta);
        [OperationContract]
        bool ActualizarReceta(int idReceta, string nombre, string descripcionPreparación);

    }
}
