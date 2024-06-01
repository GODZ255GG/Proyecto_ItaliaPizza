using System.ServiceModel;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]

    public class InventarioDelInsumo
    {
        public int _idInventarioDelInsumo { get; set; }
        public int _idInsumos { get; set; }
        public int _cantidadTotal { get; set; }

        #region Propiedades
        [DataMember]
        public int IdInventarioDelInsumo { get { return _idInventarioDelInsumo; } set { _idInventarioDelInsumo = value; } }

        [DataMember]
        public int IdInsumos { get { return _idInsumos; } set { _idInsumos = value; } }

        [DataMember]
        public int CantidadTotal { get { return _cantidadTotal; } set { _cantidadTotal = value; } }
        #endregion
    }
}