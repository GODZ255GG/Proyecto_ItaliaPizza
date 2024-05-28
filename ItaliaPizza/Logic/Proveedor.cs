using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Proveedor
    {
        private int _idProveedores;
        private string _nombreCompañia;
        private string _nombreContacto;
        private string _telefono;
        private string _ciudad;
        private string _direccion;
        private string _estado;
        private string _categoriaProveedor;

        [DataMember]
        public int IdProveedores { get { return _idProveedores; } set { _idProveedores = value; } }
        [DataMember]
        public string NombreCompañia { get { return _nombreCompañia; } set { _nombreCompañia = value; } }
        [DataMember]
        public string NombreContacto { get { return _nombreContacto; } set { _nombreContacto = value; } }
        [DataMember]
        public string Telefono { get { return _telefono; } set { _telefono = value; } }
        [DataMember]
        public string Ciudad { get { return _ciudad; } set { _ciudad = value; } }
        [DataMember]
        public string Direccion { get { return _direccion; } set { _direccion = value; } }
        [DataMember]
        public string Estado { get { return _estado; } set { _estado = value; } }
        [DataMember]
        public string CategoriaProveedor { get { return _categoriaProveedor; } set { _categoriaProveedor = value; } }
    }
}
