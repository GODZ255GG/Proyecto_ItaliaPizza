using System.ServiceModel;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]

    public class InventarioDeProductos
    {
        public int _idInventarioDeProductos { get; set; }
        public int _idProductos { get; set; }
        public int _cantidadTotal { get; set; }

        #region Propiedades
        [DataMember]
        public int IdInventarioDeProductos { get { return _idInventarioDeProductos; } set { _idInventarioDeProductos = value; } }

        [DataMember]
        public int IdProductos { get { return _idProductos; } set { _idProductos = value; } }

        [DataMember]
        public int CantidadTotal { get { return _cantidadTotal; } set { _cantidadTotal = value; } }
        #endregion
    }
}