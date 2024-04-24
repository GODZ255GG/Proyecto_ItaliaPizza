using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Interaction logic for DetallesPedido.xaml
    /// </summary>
    public partial class DetallesPedido : Window
    {
        ItaliaPizzaServer.OrderManagerClient pedidosServer = new ItaliaPizzaServer.OrderManagerClient();

        public Pedidos Pedido { get; set; }

        public DetallesPedido(Pedidos pedido)
        {
            InitializeComponent();
            Pedido = pedido;
            DataContext = Pedido;
            ValidarDomicilio();
        }

        private void ActualizarProductosTextBox(object sender, EventArgs e)
        {
            ActualizarProductos();
        }
        private void ActualizarEstadoPedido(object sender, EventArgs e)
        {
            ActualizarEstado();
        }

        private void ActualizarEstado()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionPedidos();

                string estadoPedido = pedidosDesdeBD.FirstOrDefault(p => p.IdPedidos == Pedido.IdPedidos)?.EstadoDelPedido;
                lblEstadoPedido.Content = estadoPedido;
            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ActualizarProductos()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionPedidos();

                StringBuilder detallesProductos = new StringBuilder();

                foreach (var pedido in pedidosDesdeBD)
                {
                    if (pedido.IdPedidos == Pedido.IdPedidos) 
                    {
                        detallesProductos.AppendLine($"{pedido.ProductosConcatenados}");
                        detallesProductos.AppendLine(); 
                        break;
                    }
                }
                tbxDetallesPedido.Text = detallesProductos.ToString();

            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValidarDomicilio()
        {
            if (Pedido.DomicilioDeEntrega == string.Empty)
            {
                lbDomicilio_Title.Visibility = Visibility.Hidden;
                txbDomicilio.Visibility = Visibility.Hidden;
            }
        }

        private void BtnModificarPedido_Clic(object sender, RoutedEventArgs e)
        {
            ModificarPedido modificarPedido = new ModificarPedido(Pedido);
            modificarPedido.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            modificarPedido.Closed += ActualizarProductosTextBox;
            modificarPedido.ShowDialog();
        }

        private void BtnRegresar_Clic(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnModificarEstado_Click(object sender, RoutedEventArgs e)
        {
            if (Pedido.EstadoDelPedido.ToString() == "Entregado" || Pedido.EstadoDelPedido.ToString() == "Cancelado")
            {
                MessageBox.Show("El estado del pedido ya no puede ser cambiado");
            }
            else
            {
                ModificarEstadoDePedido modificarEstadoDePedido = new ModificarEstadoDePedido(Pedido);
                modificarEstadoDePedido.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                modificarEstadoDePedido.Closed += ActualizarEstadoPedido;

                this.IsEnabled = false;

                bool? result = modificarEstadoDePedido.ShowDialog();

                this.IsEnabled = true;
            }
        }
    }
}
