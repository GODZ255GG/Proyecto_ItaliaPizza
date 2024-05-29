using System;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class CompraDeInventario
    {
        private int _idCompraDeInventario;
        private DateTime _fechaDeCompra;
        private string _insumosConcatenados;

        [DataMember]
        public int IdCompraDeInventario { get {  return _idCompraDeInventario;} set { _idCompraDeInventario = value; } }
        [DataMember]
        public DateTime FechaDeCompra { get {  return _fechaDeCompra; } set { _fechaDeCompra = value; } }
        [DataMember]
        public string InsumosConcatenados { get {  return _insumosConcatenados;} set { _insumosConcatenados = value; } }
    }
}
