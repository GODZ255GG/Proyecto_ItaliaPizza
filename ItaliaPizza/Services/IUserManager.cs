using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Logic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract]
    public interface IUserManager
    {
        [OperationContract]
        Logic.Empleados IniciarSesion(String correo, String contraseña);
        [OperationContract]
        bool RegistrarEmpleado(Empleados usuarios);

        [OperationContract]
        bool RegistrarClientes(Cliente usuarios);
        [OperationContract]
        bool UsuarioYaRegistrado(string correo);
        [OperationContract]
        List<Empleados> ObtenerListaUsuarios();
        [OperationContract]
        bool EliminarEmpleados(int idEmpleados);
        [OperationContract]
        bool ActualizarEmpleado(int idEmpleados, string nombre, string apellidoPaterno, string apellidoMaterno, string correo, string telefono, string contraseña, string rol);
    }
}
