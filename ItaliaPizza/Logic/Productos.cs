using System.Runtime.Serialization;

namespace Logic
{
    public class Productos
    {
        private int _idProductos;
        private string _nombre;
        private string _codigoProducto;
        private string _marca;
        private string _tipo;
        private double _precio;

        #region Propiedades
        [DataMember]
        public int IdProductos { get { return _idProductos; } set { _idProductos = value; } }
        [DataMember]
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        [DataMember]
        public string CodigoProducto { get { return _codigoProducto; } set { _codigoProducto = value; } }
        [DataMember]
        public string Marca { get { return _marca; } set { _marca = value; } }
        [DataMember]
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        [DataMember]
        public double Precio { get { return _precio; } set { _precio = value; } }
        #endregion
    }
}
