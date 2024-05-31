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
    public interface ICashRecord
    {
        [OperationContract]
        void RegistrarNuevoCorteDeCaja(DateTime fechaCorteDeCaja, double totalIngresos, double dineroRestante, string turno);
        [OperationContract]
        List<Logic.CorteDeCaja> RecuperarInformacionDeCortesDeCaja();
    }
}
