using System.ServiceModel;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class Cliente
    {
        private int _idClientes;
        private string _nombre;
        private string _telefono;
        private string _rol;


        #region Propiedades
        [DataMember]
        public int IdEmpleados { get { return _idClientes; } set { _idClientes = value; } }
        [DataMember]
        public string Nombre { get { return _nombre; } set { _nombre = value; } }

        [DataMember]
        public string Telefono { get { return _telefono; } set { _telefono = value; } }

        [DataMember]
        public string Rol { get { return _rol; } set { _rol = value; } }
        #endregion
    }
}

