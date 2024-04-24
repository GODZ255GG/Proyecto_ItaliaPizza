using System.Runtime.Serialization;

namespace Logic
{
    public class Insumos
    {
        private int _idInsumos;
        private string _nombre;
        private string _codigoInsumo;
        private string _marca;
        private string _tipo;
        private string _cantidadDeEmpaque;

        #region Propiedades
        [DataMember]
        public int IdInsumos { get { return _idInsumos; } set { _idInsumos = value; } }
        [DataMember]
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        [DataMember]
        public string CodigoInsumo { get { return _codigoInsumo; } set { _codigoInsumo = value; } }
        [DataMember]
        public string Marca { get { return _marca; } set { _marca = value; } }
        [DataMember]
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        [DataMember]
        public string CantidadDeEmpaque { get { return _cantidadDeEmpaque; } set { _cantidadDeEmpaque = value; } }
        #endregion
    }
}