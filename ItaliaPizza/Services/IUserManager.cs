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
        bool ExisteCorreoYContraseña(string correo, string contraseña);
    }
}
