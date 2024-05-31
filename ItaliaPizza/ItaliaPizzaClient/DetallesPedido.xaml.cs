using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using log4net;
using Logs;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Interaction logic for DetallesPedido.xaml
    /// </summary>
    public partial class DetallesPedido : Window
    {
        private static readonly ILog Log = Logger.GetLogger();
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
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
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
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
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
                Utilidades.Utilidades.MostrarMensaje("El estado del pedido ya no puede ser cambiado", "Cambio de Estado no permitido", MessageBoxImage.Warning);
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
