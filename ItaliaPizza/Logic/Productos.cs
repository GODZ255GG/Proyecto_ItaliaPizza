using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Productos
    {
        private int _idProductos;
        private string _nombre;
        private string _marca;
        private string _tipo;
        private string _foto;
        private float _precio;
        private int _stock;

        #region Propiedades
        [DataMember]
        public int IdProductos { get { return _idProductos; } set { _idProductos = value; } }
        [DataMember]
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        [DataMember]
        public string Marca { get { return _marca; } set { _marca = value; } }
        [DataMember]
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        [DataMember]
        public string Foto { get { return _foto; } set { _foto = value; } }
        [DataMember]
        public float Precio { get { return _precio; } set { _precio = value; } }
        [DataMember]
        public int Stock { get { return _stock; } set { _stock = value; } }
        #endregion
    }
}
