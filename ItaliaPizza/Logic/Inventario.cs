using System.ServiceModel;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]

    public class Inventario
    {
        public int _idInventario { get; set; }
        public int _cantidadMaxima { get; set; }
        public int _cantidadMinima { get; set; }

        #region Propiedades
        [DataMember]
        public int IdInventario { get { return _idInventario; } set { _idInventario = value; } }

        [DataMember]
        public int CantidadMaxima { get { return _cantidadMaxima; } set { _cantidadMaxima = value; } }

        [DataMember]
        public int CantidadMinima { get { return _cantidadMinima; } set { _cantidadMinima = value; } }
        #endregion
    }
}
