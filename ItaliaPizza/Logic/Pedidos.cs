using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [DataContract]
    public class Pedidos
    {
        private int _idPedidos;
        private double? _total;
        private int _idClientes;
        private string _nombreDelCliente;
        private string _domicilioDeEntrega;
        private string _tipoDePedido;
        private string _estadoDelPedido;
        private string _productosConcatenados;
        private DateTime _fechaPedido;

        [DataMember]
        public int IdPedidos { get { return _idPedidos; } set { _idPedidos = value; } }

        [DataMember]
        public double? Total { get { return _total; } set { _total = value; } }

        [DataMember]
        public int IdClientes { get { return _idClientes; } set { _idClientes = value; } }

        [DataMember]
        public string NombreDelCliente { get { return _nombreDelCliente; } set { _nombreDelCliente = value; } }

        [DataMember]
        public string DomicilioDeEntrega { get { return _domicilioDeEntrega; } set { _domicilioDeEntrega = value; } }

        [DataMember]
        public string TipoDePedido { get { return _tipoDePedido; } set { _tipoDePedido = value; } }

        [DataMember]
        public string EstadoDelPedido { get { return _estadoDelPedido; } set { _estadoDelPedido = value; } }

        [DataMember]
        public string ProductosConcatenados { get { return _productosConcatenados; } set { _productosConcatenados = value; } }

        [DataMember]
        public DateTime FechaPedido { get { return _fechaPedido; } set { _fechaPedido = value; } }
    }
}